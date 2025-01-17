using AutoMapper;
using EcWebapi.Database.Table;
using EcWebapi.Dto;
using EcWebapi.Dto.Member;

namespace EcWebapi
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<MemberDto, Member>();

            CreateMap<UpdateMemberDto, Member>();

            CreateMap<CreateMemberDto, Member>();

            CreateMap<ApiResponse, ApiResponseErrorDto>();
        }
    }
}