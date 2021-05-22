using System.Linq;
using API.DTOs;
using API.Entites;
using API.Extensions;
using AutoMapper;

namespace API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        //this profiler is used for to map the class with their properties
        public AutoMapperProfiles()
        {
            CreateMap<Appuser,MemberDto>()
            .ForMember(dest=>dest.PhotoUrl,opt=>opt.
            MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url))//used for map the specific property
            .ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDto>();
            CreateMap<MemberUpdateDto,Appuser>();
            CreateMap<RegisterDto,Appuser>();
        }
    }
}