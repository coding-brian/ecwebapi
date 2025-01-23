namespace EcWebapi.Dto
{
    public class ProductImageDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ProductId { get; set; }

        public string Url { get; set; }

        public bool IsBanner { get; set; }

        public int Priority { get; set; }

        public bool IsActive { get; set; }

        public bool IsDesktopSize { get; set; }

        public bool IsMobileSize { get; set; }

        public bool IsTabletSize { get; set; }
    }
}