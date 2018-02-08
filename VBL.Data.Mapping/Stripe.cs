using AutoMapper;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public class StripeAccountDetailsDTO
    {
        public string BusinessName { get; set; }
        public string DisplayName { get; set; }
        public bool LiveMode { get; set; }
    }
    public class StripePaymentTokenDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("card_id")]
        public string CardId { get; set; }
        [JsonProperty("card_last4")]
        public string CardLast4 { get; set; }
        [JsonProperty("card_brand")]
        public string CardBrand { get; set; }
        [JsonProperty("client_ip")]
        public string ClientIP { get; set; }
    }

    public class StripeAccountProfile : Profile
    {
        public StripeAccountProfile()
        {
            CreateMap<StripeOAuthToken, StripeAuthToken>()
                .ReverseMap();

            CreateMap<StripeAccount, StripeAccountDetails>();

            CreateMap<StripeAccountDetails, StripeAccountDetailsDTO>()
                .ForMember(m => m.LiveMode, opt => opt.MapFrom(s => s.StripeAuthToken.LiveMode));

            CreateMap<StripePaymentTokenDTO, StripePaymentToken>();
        }
    }
}
