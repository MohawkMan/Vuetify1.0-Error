using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public int OrganizationId { get; set; }
        public OrganizationDTOSkinny Organization { get; set; }
        public List<TournamentDivisionDTO> Divisions { get; set; }
    }

    public class TournamentProfile: Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentDTO>()
                .ReverseMap()
                .ForMember(s => s.Organization, opt => opt.Ignore());
        }
    }
}
