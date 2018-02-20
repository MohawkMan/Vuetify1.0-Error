using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentDTO
    {
        public string Description { get; set; }
        public List<TournamentDivisionDTO> Divisions { get; set; }
        public string ExternalRegistrationUrl { get; set; }
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public OrganizationDTOSkinny Organization { get; set; }
        public int OrganizationId { get; set; }
        public int StatusId { get; set; }
        public TournamentDivisionDTO DivisionTemplate { get; set; }
    }
    public partial class TournamentDTOIncoming
    {
        public string Description { get; set; }
        public List<TournamentDivisionDTOIncoming> Divisions { get; set; }
        public string ExternalRegistrationUrl { get; set; }
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }
        public int StatusId { get; set; }
    }

    public class TournamentSummaryDTO
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Name { get; set; }
        public List<string> Locations { get; set; }
        public OrganizationDTOSkinny Organization { get; set; }
        public bool IsPublic { get; set; }
        public string SanctionedBy { get; set; }
        public int StatusId { get; set; }

    }
    public class TournamentProfile: Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentDTO>()
                .ReverseMap()
                .ForMember(s => s.Organization, opt => opt.Ignore());

            CreateMap<TournamentDTOIncoming, Tournament>().EqualityComparison((odto, o) => odto.Id == o.Id);

            CreateMap<Tournament, TournamentSummaryDTO>();
        }
    }
}
