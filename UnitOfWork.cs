using EcWebapi.Database;
using EcWebapi.Database.Table;
using EcWebapi.Repository;

namespace EcWebapi
{
    public class UnitOfWork(EcDbContext context,
                            GenericRepository<Member> memberRepository,
                            GenericRepository<MemberCaptcha> memberCaptchaRepository,
                            GenericRepository<ApiResponse> apiResponseRepository,
                            GenericRepository<Store> storeRepository,
                            GenericRepository<News> newsRepository,
                            GenericRepository<Product> productRepository,
                            GenericRepository<SocialMedia> socialMediaRepository,
                            GenericRepository<ProductCategoryImage> productCategoryImageRepository,
                            GenericRepository<ProductCategory> productCategoryRepository,
                            GenericRepository<NewsImage> newsImageRepository,
                            GenericRepository<ProductImage> productImageRepository,
                            GenericRepository<ProductCategoryMapping> productCategoryMappingRepository,
                            GenericRepository<ProductPrice> productPriceRepository,
                            GenericRepository<ProductContent> productContentRepository,
                            GenericRepository<ProductGalleryImage> productGalleryImageRepository,
                            GenericRepository<ProductRelationMapping> productRelationMappingRepository,
                            GenericRepository<ProductRelationImage> productRelationImageRepository) : IDisposable
    {
        private readonly EcDbContext _context = context;

        public GenericRepository<Member> MemberRepository = memberRepository;

        public GenericRepository<MemberCaptcha> MemberCaptchaRepository = memberCaptchaRepository;

        public GenericRepository<ApiResponse> ApiResponseRepository = apiResponseRepository;

        public GenericRepository<Store> StoreRepository = storeRepository;

        public GenericRepository<News> NewsRepository = newsRepository;
        public GenericRepository<Product> ProductRepository = productRepository;
        public GenericRepository<SocialMedia> SocialMediaRepository = socialMediaRepository;
        public GenericRepository<ProductCategory> ProductCategoryRepository = productCategoryRepository;
        public GenericRepository<ProductCategoryImage> ProductCategoryImageRepository = productCategoryImageRepository;
        public GenericRepository<NewsImage> NewsImageRepository = newsImageRepository;
        public GenericRepository<ProductImage> ProductImageRepository = productImageRepository;
        public GenericRepository<ProductCategoryMapping> ProductCategoryMappingRepository = productCategoryMappingRepository;
        public GenericRepository<ProductPrice> ProductPriceRepository = productPriceRepository;
        public GenericRepository<ProductContent> ProductContentRepository = productContentRepository;
        public GenericRepository<ProductGalleryImage> ProductGalleryImageRepository = productGalleryImageRepository;
        public GenericRepository<ProductRelationMapping> ProductRelationMappingRepository = productRelationMappingRepository;
        public GenericRepository<ProductRelationImage> ProductRelationImageRepository = productRelationImageRepository;

        private bool disposed = false;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}