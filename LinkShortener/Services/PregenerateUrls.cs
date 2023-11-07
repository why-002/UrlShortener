using LinkShortener.Jobs;
using Quartz;

namespace LinkShortener.Services
{
    public class PregenerateUrls
    {
        private List<string> shortUrls { get; set; }
        private readonly UrlGenerator urlShortener;
        private IScheduler _scheduler;
        private readonly ISchedulerFactory _schedulerFactory;
        public PregenerateUrls(ISchedulerFactory schedulerFactory) {
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
            string temp;
            if (shortUrls.Count() > 50)
            {
                temp = shortUrls.First();
                shortUrls.RemoveAt(0);
                return temp;
            }

            temp = shortUrls.FirstOrDefault("");
            if(temp != "")
            {
                shortUrls.RemoveAt(0);
            }

            await _scheduler.TriggerJob(new JobKey("name", "group"));
            return temp;
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
