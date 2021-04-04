using API.DTOs;
using API.Entites;
using AutoMapper;

namespace API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Appuser,MemberDto>();
            CreateMap<Photo,PhotoDto>();
        }
    }
}