using LinkShortener.Jobs;
using Quartz;

namespace LinkShortener.Services
{
    public class PregeneratedUrlShortener
    {
        private List<string> shortUrls { get; set; }
        private readonly UrlShortener urlShortener;
        private IScheduler _scheduler;
        private readonly ISchedulerFactory _schedulerFactory;
        public PregeneratedUrlShortener(ISchedulerFactory schedulerFactory) {
            _schedulerFactory = schedulerFactory;
            shortUrls = new List<string>();
        }

        public async Task<string> GetShortUrl()
        {
            if(_scheduler is null)
            {
                _scheduler = await _schedulerFactory.GetScheduler();
                var job = JobBuilder.Create<GenerateShortUrlsJob>()
            .WithIdentity("name", "group")
            .Build();

                var replace = true;
                var durable = true;
                await _scheduler.AddJob(job, replace, durable);
                await _scheduler.Start();
            }
            if (shortUrls.Count() > 0)
            {
                var temp = shortUrls.First();
                shortUrls.RemoveAt(0);
                return temp;
            }
            await _scheduler.TriggerJob(new JobKey("name", "group"));
            return "";
        }
        public void AddShortUrl(string url)
        {
            shortUrls.Add(url);
        }
        public int GetCount()
        {
            return shortUrls.Count();
        }
    }
}
