using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;

namespace VBL.Core
{
    public partial class PayPalManager
    {
        private readonly IMapper _mapper;
        private readonly VBLDbContext _db;
        private readonly ILogger _logger;
        private readonly VblConfig _config;
        private static readonly HttpClient client = new HttpClient();


        public PayPalManager(IMapper mapper, VBLDbContext db, ILogger<TournamentManager> logger, IOptions<VblConfig> config)
        {
            _mapper = mapper;
            _db = db;
            _logger = logger;
            _config = config.Value;
        }

        public async Task<PayPalTokenDTO> GetTokenAsync(PayPalTransaction transaction)
        {
            var returnToken = new PayPalTokenDTO
            {
                PostUrl = _config.PayPal.TransactionUrlDev,
                SecureTokenId = transaction.Id
            };
            var parameters = GetApiAuthenticationParams() +
                GetTransparentRedirectParams(transaction.Id) +
                GetTransactionParams(transaction);

            client.DefaultRequestHeaders.Add("X-VPS-REQUEST-ID", transaction.Id.ToString());
            client.DefaultRequestHeaders.Add("X-VPS-CLIENT-TIMEOUT", "45");
            //Post to paypal
            var response = await client.PostAsync(_config.PayPal.TokenUrl, new StringContent(parameters, Encoding.UTF8, "text/namevalue"));
            //read response
            var rawResponse = await response.Content.ReadAsStringAsync();

            var token = new PayPalToken(rawResponse);
            transaction.Tokens.Add(token);
            returnToken.SecureToken = token.SecureToken;

            return returnToken;
        }
        private string GetApiAuthenticationParams()
        {
            var paypalUser = _config.PayPal.User;
            var paypalVendor = _config.PayPal.Vendor;
            var paypalPartner = _config.PayPal.Partner;
            var paypalPw = _config.AppKeys.PayPal;

            var apiParams = @"USER=" + paypalUser
                            + "&VENDOR=" + paypalVendor
                            + "&PARTNER=" + paypalPartner
                            + "&PWD=" + paypalPw
                            + "&VERBOSITY=HIGH";

            // find more appropriate place for this param
            //+ "&VERBOSITY=HIGH";

            return apiParams;
        }
        private string GetTransparentRedirectParams(int transactionId)
        {
            var transparentParams = @"&SILENTTRAN=TRUE" +
                                   "&CREATESECURETOKEN=Y" +
                                   "&SECURETOKENID=" + transactionId.ToString();

            return transparentParams;
        }
        private string GetTransactionParams(PayPalTransaction transaction)
        {
            var transactionParams = @"&TRXTYPE=" + "S"
                                    + "&TENDER=C"
                                    + "&CURRENCY=USD"
                                    //+ "&AMT=105.02"
                                    + "&AMT=" + transaction.Total.ToString("N2")
                                    + "&ERRORURL= " + _config.PayPal.ErrorUrl
                                    + "&CANCELURL=" + _config.PayPal.CancelUrl
                                    + "&RETURNURL=" + _config.PayPal.ReturnUrl;

            return transactionParams;
        }

    }
    public class PayPalTokenDTO
    {
        public string PostUrl { get; set; }
        public string SecureToken { get; set; }
        public int SecureTokenId { get; set; }
        public string GotoUrl { get; set; }
    }
}
