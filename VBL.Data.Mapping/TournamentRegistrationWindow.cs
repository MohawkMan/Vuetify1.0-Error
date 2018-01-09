﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentRegistrationWindowDTO
    {
        public int Id { get; set; }
        public double? Fee { get; set; }
        public bool FeeIsPerTeam { get; set; }
        public string StartDate { get; set; }
        public string StartDateFormatted { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndDateFormatted { get; set; }
        public string EndTime { get; set; }
        public bool IsEarly { get; set; }
        public bool IsLate { get; set; }
        public bool CanPayAtEvent { get; set; }
        public bool CanProcessPayment { get; set; }
        //public bool IsCurrent { get; set; }
        //public bool IsOpen { get; set; }

    }

    public class TournamentRegistrationWindowProfile : Profile
    {
        public TournamentRegistrationWindowProfile()
        {
            CreateMap<TournamentRegistrationWindow, TournamentRegistrationWindowDTO>()
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.DtStart.Value.ToString("yyyy-MM-dd")))
                .ForMember(d => d.StartDateFormatted, opt => opt.MapFrom(s => s.DtStart.Value.ToString("MM/dd/yyyy")))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.DtStart.Value.ToString("h:mmtt").ToLower()))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.DtEnd.Value.ToString("yyyy-MM-dd")))
                .ForMember(d => d.EndDateFormatted, opt => opt.MapFrom(s => s.DtEnd.Value.ToString("MM/dd/yyyy")))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.DtEnd.Value.ToString("h:mmtt").ToLower()))
                //.ForMember(d => d.IsCurrent, opt => opt.Ignore())
                //.ForMember(d => d.IsOpen, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(s => s.DtStart, opt => opt.MapFrom(d => d.StartDate + " " + d.StartTime))
                .ForMember(s => s.DtEnd, opt => opt.MapFrom(d => d.EndDate + " " + d.EndTime))
                ;
        }
    }
}
