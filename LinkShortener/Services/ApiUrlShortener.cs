using Microsoft.EntityFrameworkCore;
using System;

namespace LinkShortener.Services
{
    public class ApiUrlShortener
    {
        private readonly PregeneratedUrlShortener _pregenerator;
        private readonly UrlShortener _urlShortener;
        public ApiUrlShortener(PregeneratedUrlShortener pregen, UrlShortener urlShortener) 
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
