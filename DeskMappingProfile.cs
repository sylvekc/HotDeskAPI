using AutoMapper;
using HotDeskAPI.Entities;
using HotDeskAPI.Models;

namespace HotDeskAPI
{
    public class DeskMappingProfile : Profile
    {
        public DeskMappingProfile()
        {
            CreateMap<AddDeskDto, Desk>();
        }
    }
}
