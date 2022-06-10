using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppAndon, AndonDto>();
            CreateMap<AndonUpdateDto, AppAndon>();
            
            CreateMap<AppNodeList, NodeListDto>();
            CreateMap<NodeListRegisterDto, AppNodeList>();

            //CreateMap<AppUser, UserDto>();
            CreateMap<UserRegisterDto, AppUser>();
        }
    } 
}