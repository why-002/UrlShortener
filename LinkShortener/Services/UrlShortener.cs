namespace LinkShortener.Services
{
    public class UrlShortener
    {
        public const int NumberOfCharsShortLink = 8;
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly Random _random = new Random();
        private readonly LinkShortenerDbContext _context;

        public UrlShortener(LinkShortenerDbContext context)
        {
            _context = context;
        }

        public string GenerateShortUrlCode()
        {
            var chars = new char[NumberOfCharsShortLink];

            for (int i=0; i<NumberOfCharsShortLink; i++)
            {
                chars[i] = Alphabet[_random.Next(Alphabet.Length)];
            }

            var code = new string(chars);

            if(!_context.ShortenedUrls.Any(s => s.ShortUrlCode == code))
            {
                return code;
            }

            return GenerateShortUrlCode();
        }
    }
}
