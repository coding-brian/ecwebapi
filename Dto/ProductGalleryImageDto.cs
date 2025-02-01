namespace EcWebapi.Dto
{
    public class ProductGalleryImageDto : EntityDto
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }

        public int Priority { get; set; }
        public bool IsDesktopSize { get; set; }

        public bool IsMobileSize { get; set; }

        public bool IsTabletSize { get; set; }

        public bool IsActive { get; set; }
    }
}