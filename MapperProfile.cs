using AutoMapper;
using EcWebapi.Database.Table;
using EcWebapi.Dto;

namespace EcWebapi
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<MemberDto, Member>();
        }
    }
}