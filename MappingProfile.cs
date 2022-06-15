﻿using AutoMapper;
using HotDeskAPI.Entities;
using HotDeskAPI.Models;

namespace HotDeskAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddDeskDto, Desk>();
            CreateMap<AddLocationDto, Location>();
            CreateMap<AddReservationDto, Reservation>();
        }
    }
}
