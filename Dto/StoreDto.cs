namespace EcWebapi.Dto
{
    public class StoreDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string LogoUrl { get; set; }

        public int ShippingFee { get; set; }

        public int TaxRate { get; set; }

        public bool IsActive { get; set; }

        public IList<SocialMediaDto> SocialMedias { get; set; }
    }
}