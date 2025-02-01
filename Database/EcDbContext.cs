using EcWebapi.Database.Table;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcWebapi.Database
{
    public class EcDbContext(DbContextOptions<EcDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberCaptcha> MemberCaptchas { get; set; }
        public DbSet<ApiResponse> ApiResponses { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryImage> ProductCategoryImages { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }
        public DbSet<ProductContent> ProductContents { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductGalleryImage> ProductGalleryImages { get; set; }
        public DbSet<ProductRelationMapping> ProductRelationMappings { get; set; }
        public DbSet<ProductRelationImage> ProductRelationImages { get; set; }
    }
}