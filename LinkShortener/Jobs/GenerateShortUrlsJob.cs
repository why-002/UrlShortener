using LinkShortener.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;

namespace LinkShortener.Jobs
{
    public class GenerateShortUrlsJob : IJob
    {
        private readonly PregeneratedUrlShortener _pregenerator;
        private readonly UrlShortener _urlShortener;
        private readonly LinkShortenerDbContext _context; 
        public GenerateShortUrlsJob(PregeneratedUrlShortener pregen, UrlShortener urlShortener) 
        {
            _pregenerator = pregen;
            _urlShortener = urlShortener;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            while(_pregenerator.GetCount() < 1000)
            {
                _pregenerator.AddShortUrl(_urlShortener.GenerateShortUrlCode());
                System.Diagnostics.Debug.WriteLine("Added shortUrl to list");
            }
        }
    }
}
