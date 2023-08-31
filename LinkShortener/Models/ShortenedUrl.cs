namespace LinkShortener.Models
{
    public class ShortenedUrl
    {
        public Guid Id { get; set; }
        public string LongUrl { get; set; } = string.Empty;
        public string ShortUrlCode { get; set; } = string.Empty;
        public DateTime Created { get; set; }

    }
}
