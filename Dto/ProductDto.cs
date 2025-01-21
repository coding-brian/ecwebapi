namespace EcWebapi.Dto
{
    public class ProductDto
    {
        public string Code { get; set; }

        public Guid? SkuId { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsInBanner { get; set; }

        public bool SNewProduct { get; set; }

        public bool SInHomepage { get; set; }
    }
}