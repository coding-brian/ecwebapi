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
            CreateMap<Store, StoreDto>();

            CreateMap<NewsDto, News>();
            CreateMap<News, NewsDto>();

            CreateMap<Product, ProductDto>();

            CreateMap<SocialMedia, SocialMediaDto>();

            CreateMap<ProductCategory, ProductCategoryDto>();

            CreateMap<ProductCategoryImage, ProductCategoryImageDto>();
        }
    }
}