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
            CreateMap<AndonUpdateDto, Andon>();
            
            CreateMap<NodeList, NodeListDto>();
            CreateMap<NodeListRegisterDto, NodeList>();

            //CreateMap<AppUser, UserDto>();
            CreateMap<UserRegisterDto, AppUser>();
        }
    } 
}