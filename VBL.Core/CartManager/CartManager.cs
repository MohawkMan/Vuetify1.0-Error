using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;
using VBL.Data.Mapping;

namespace VBL.Core
{
    public partial class CartManager
    {
        private readonly IMapper _mapper;
        private readonly VBLDbContext _db;
        private readonly ILogger _logger;
        private readonly VblConfig _config;
        private readonly StripeManager _stripe;
        private readonly TournamentManager _tournamentManager;

        public CartManager(IMapper mapper, VBLDbContext db, ILogger<TournamentManager> logger, IOptions<VblConfig> config, StripeManager stripe, TournamentManager tournamentManager)
        {
            _mapper = mapper;
            _db = db;
            _logger = logger;
            _config = config.Value;
            _stripe = stripe;
            _tournamentManager = tournamentManager;
        }

        public async Task<ShoppingBag> ProcessBag(ShoppingBagDTO dto, bool skipPayment = false)
        {
            //save the bag
            var bag = await SaveBag(dto);
            //process the payment
            if(!skipPayment)
            {
                var payment = await _stripe.ProcessBagPayment(bag);
            }
            foreach (var item in bag.Items)
            {
                if(! string.IsNullOrWhiteSpace(item.RawRegistrationData))
                {
                    var trDTO = JsonConvert.DeserializeObject<TournamentRegistrationDTO>(item.RawRegistrationData);
                    var registration = await _tournamentManager.Register(trDTO, true);
                    item.TournamentRegistrationId = registration.Id;
                    item.TournamentRegistration = registration;
                }
            }
            await _db.SaveChangesAsync();
            return bag;
        }
        private async Task<ShoppingBag> SaveBag(ShoppingBagDTO dto)
        {
            var bag = new ShoppingBag();
            _mapper.Map(dto, bag);
            _db.ShoppingBags.Add(bag);
            await _db.SaveChangesAsync();
            return bag;
        }
    }
}
