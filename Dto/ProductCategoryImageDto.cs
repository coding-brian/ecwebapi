namespace EcWebapi.Dto
{
    public class ProductCategoryImageDto
    {
        public string Url { get; set; }

        public int Priority { get; set; }

        public Guid ProductCategoryId { get; set; }

        public bool IsInBanner { get; set; }

        public bool IsActive { get; set; }

        public bool IsDesktopSize { get; set; }

        public bool IsMobileSize { get; set; }

        public bool IsTabletSize { get; set; }
    }
}