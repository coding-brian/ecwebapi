using AutoMapper;
using EcWebapi.Database.Table;
using EcWebapi.Dto;
using EcWebapi.Dto.Member;
using EcWebapi.Dto.Order;
using EcWebapi.Dto.OrderDetail;
using EcWebapi.Dto.Product;

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

            CreateMap<NewsImage, NewsImageDto>();
            CreateMap<ProductImage, ProductImageDto>();

            CreateMap<ProductContent, ProductContentDto>();
            CreateMap<ProductPrice, ProductPriceDto>();

            CreateMap<ProductGalleryImage, ProductGalleryImageDto>();
            CreateMap<ProductRelationImage, ProductRelationImageDto>();
            CreateMap<ProductRelationMapping, ProductRelationMappingDto>();
            CreateMap<PaymentMethod, PaymentMethodDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>();

            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateOrderShipmentDto, OrderShipment>();
            CreateMap<CreateOrderDetailDto, OrderDetail>();
            CreateMap<CreateOrderCreditCardDto, OrderCreditCard>();
        }
    }
}