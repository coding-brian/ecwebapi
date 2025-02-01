namespace EcWebapi.Dto
{
    public class ProductRelationImageDto : EntityDto
    {
        public Guid ProductRelationId { get; set; }

        public string Url { get; set; }

        public bool IsActive { get; set; }

        public bool IsDesktopSize { get; set; }

        public bool IsMobileSize { get; set; }

        public bool IsTabletSize { get; set; }
    }
}