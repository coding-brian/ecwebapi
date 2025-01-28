namespace EcWebapi.Dto
{
    public class NewsDto : EntityDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public IList<NewsImageDto> Images { get; set; }
    }
}