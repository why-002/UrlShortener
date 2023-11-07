namespace LinkShortener.Services
{
    public class UrlGenerator
    {
        public const int NumberOfCharsShortLink = 7;
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly Random _random = new Random();
        private readonly LinkShortenerDbContext _context;
        private readonly PregenerateUrls _pregeneratedUrlShortener;

        public UrlGenerator(LinkShortenerDbContext context)
        {
            _context = context;
        }

        public string GenerateShortUrlCode()
        {
            string code = string.Empty;
            var needsNewLink = true;
            while (needsNewLink)
            {
                var chars = new char[NumberOfCharsShortLink];

                for (int i = 0; i < NumberOfCharsShortLink; i++)
                {
                    chars[i] = Alphabet[_random.Next(Alphabet.Length)];
                }

                code = new string(chars);

                needsNewLink = _context.ShortenedUrls.Any(s => s.ShortUrlCode == code);
            }
            return code;
        }
    }
}
