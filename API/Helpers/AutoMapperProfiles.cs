using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Andon, AndonDto>();
            CreateMap<AndonRegisterDto, Andon>();
            CreateMap<Andon, AndonGroupedDto>();
            
            CreateMap<NodeList, NodeListDto>();
            CreateMap<NodeListRegisterDto, NodeList>();
            CreateMap<NodeListGetDto, NodeList>();
        }
    } 
}