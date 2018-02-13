using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;
using Hangfire;
using Newtonsoft.Json;
using VBL.Data.Mapping;

namespace VBL.Core
{
    public partial class StripeManager
    {
        private readonly IMapper _mapper;
        private readonly VBLDbContext _db;
        private readonly ILogger _logger;
        private readonly VblConfig _config;
        private static readonly HttpClient client = new HttpClient();

        public StripeManager(IMapper mapper, VBLDbContext db, ILogger<TournamentManager> logger, IOptions<VblConfig> config)
        {
            _mapper = mapper;
            _db = db;
            _logger = logger;
            _config = config.Value;
        }

        public async Task<string> GetConnectURL(int userId, int organizationId)
        {
            var clickRecord = new StripeConnectClick
            {
                ApplicationUserId = userId,
                OrganizationId = organizationId
            };
            _db.StripeConnectClicks.Add(clickRecord);
            await _db.SaveChangesAsync();

            return $"{_config.Stripe.LiveConnectURL}&state={clickRecord.Id}";
        }
        public async Task<string> GetReturnURL(string stripeConnectClickId)
        {
            var clickRecord = await _db.StripeConnectClicks
                .Include(i => i.Organization)
                .FirstOrDefaultAsync(f => f.Id == stripeConnectClickId);

            if (clickRecord == null)
                return _config.BaseURL;

            return $"{_config.BaseURL}/{clickRecord.Organization.Username}/admin/settings";
        }
        public async Task<string> Connect(string code, string stripeConnectClickId, string scope)
        {
            // GET CLICK RECORD
            var clickRecord = await _db.StripeConnectClicks
                .Include(i => i.Organization)
                .Include(i => i.StripeAuthToken)
                .FirstOrDefaultAsync(f => f.Id == stripeConnectClickId);

            if (clickRecord == null)
                throw new Exception("Could not find click record");

            clickRecord.Code = code;
            clickRecord.Scope = scope;

            //CREATE OR UPDATE TOKEN RECORD INCLUDING ACCOUNT DETAILS
            var token = await FetchUserCredentials(clickRecord.Code);
            //ATTACH TO CLICK RECORD
            clickRecord.StripeAuthToken = token;

            //CHECK FOR ERROR
            if (!string.IsNullOrWhiteSpace(token.Error))
            {
                //ERRORED RETURN ERROR
                return $"{_config.BaseURL}/{clickRecord.Organization.Username}/admin/settings?error={token.Error}&error_description={token.ErrorDescription}";
            }

            //NO ERROR
            if(token.AccountDetails == null)
            {
                token.AccountDetails = new StripeAccountDetails
                {
                    Id = token.StripeUserId,
                    IsDefault = !await _db.StripeAccountDetails.AnyAsync(a => a.OrganizationId == clickRecord.OrganizationId)
                };
            }
            token.AccountDetails.OrganizationId = clickRecord.OrganizationId;
            await _db.SaveChangesAsync();

            BackgroundJob.Enqueue<StripeManager>(s => s.FetchAccountDetails(token.StripeUserId));

            return $"{_config.BaseURL}/{clickRecord.Organization.Username}/admin/settings";
        }

        private async Task<StripeAuthToken> FetchUserCredentials(string code)
        {
            var stripe = new StripeOAuthTokenService(_config.AppKeys.Stripe);
            var options = new StripeOAuthTokenCreateOptions
            {
                Code = code,
                ClientSecret = _config.AppKeys.Stripe,
                GrantType = "authorization_code"
            };
            var newToken = await stripe.CreateAsync(options);

            StripeAuthToken token = null;
            if(!string.IsNullOrWhiteSpace(newToken.StripeUserId))
            {
                token = await _db.StripeAuthTokens
                    .Include(i => i.AccountDetails)
                    .Where(w => w.StripeUserId == newToken.StripeUserId)
                    .FirstOrDefaultAsync();
            }
            if (token == null)
            {
                token = new StripeAuthToken();
                _db.StripeAuthTokens.Add(token);
            }
            _mapper.Map(newToken, token);
            return token;
        }

        [Queue("default")]
        public async Task FetchAccountDetails(string stripeUserId)
        {
            var details = await _db.StripeAccountDetails.FindAsync(stripeUserId);
            if(details == null)
            {
                throw new Exception($"Account with Id: {stripeUserId} does not exist");
            }

            StripeConfiguration.SetApiKey(_config.AppKeys.Stripe);

            var accountService = new StripeAccountService();
            StripeAccount account = accountService.Get(stripeUserId);

            _mapper.Map(account, details);
            await _db.SaveChangesAsync();
        }

        public async Task<StripeChargeRecord> ProcessBagPayment(ShoppingBag bag)
        {
            var partnerAccount = await GetPaymentAccountInfo(bag.OrganizationId);
            var appFee = bag.Items.Count(c => !string.IsNullOrWhiteSpace(c.RawRegistrationData)) * 100;
            var desc = $"Volleyball Life OrderId: {bag.DtCreated.Value.ToString("yyyyMMdd")}-{bag.Id}";
            if(bag.Items.Count() == 1)
            {
                desc = bag.Items[0].Description;
            }

            var requestOptions = new StripeRequestOptions();
            requestOptions.ApiKey = _config.AppKeys.Stripe;
            requestOptions.StripeConnectAccountId = partnerAccount.Id;

            var chargeOptions = new StripeChargeCreateOptions()
            {
                Amount = Convert.ToInt32(bag.Total * 100),
                Currency = "usd",
                Description = $"Volleyball Life OrderId: {bag.DtCreated.Value.ToString("yyyyMMdd")}-{bag.Id}",
                SourceTokenOrExistingSourceId = bag.PaymentToken.Id,
                ReceiptEmail = bag.EmailReceiptTo,
                ApplicationFee = appFee
            };

            var chargeService = new StripeChargeService();
            var payment = await chargeService.CreateAsync(chargeOptions, requestOptions);
            var record = new StripeChargeRecord()
            {
                Id = payment.Id,
                RawResponse = payment.StripeResponse.ResponseJson,
                ShoppingBag = bag,
                ShoppingBagId = bag.Id
            };
            _db.StripeChargeRecords.Add(record);
            await _db.SaveChangesAsync();
            return record;
        }
        private async Task<StripeAccountDetails> GetPaymentAccountInfo(int organizationId)
        {
            return await _db.StripeAccountDetails
                .Where(w => w.OrganizationId == organizationId && w.IsDefault && w.StripeAuthToken.LiveMode)
                .FirstOrDefaultAsync();
        }
    }
}
