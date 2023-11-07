using Microsoft.EntityFrameworkCore;
using System;

namespace LinkShortener.Services
{
    public class UrlShortener
    {
        private readonly PregenerateUrls _pregenerator;
        private readonly UrlGenerator _urlShortener;
        public UrlShortener(PregenerateUrls pregen, UrlGenerator urlShortener) 
        {
            _pregenerator = pregen;
            _urlShortener = urlShortener;
        }
        public async Task<string> GenerateShortUrlCode()
        {
            var code = await _pregenerator.GetShortUrl();
            if (!String.IsNullOrEmpty(code))
            {
                return code;
            }
            else
            {
                return _urlShortener.GenerateShortUrlCode();
            }
        }
    }
}
