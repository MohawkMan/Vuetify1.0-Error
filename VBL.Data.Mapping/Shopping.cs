using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public class ShoppingBagDTO
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public List<ShoppingBagItemDTO> Items { get; set; } = new List<ShoppingBagItemDTO>();
        public StripePaymentTokenDTO PaymentToken { get; set; }
        public double Total { get; set; }
        public string EmailReceiptTo { get; set; }
    }
    public partial class ShoppingBagItemDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public TournamentRegistrationDTO Registration { get; set; }
        public string Product { get; set; }
    }
    public class ShoppingProfile: Profile
    {
        public ShoppingProfile()
        {
            CreateMap<ShoppingBagDTO, ShoppingBag>();

            CreateMap<ShoppingBagItemDTO, ShoppingBagItem>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.RawRegistrationData, opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.Registration)));
        }
    }
}
