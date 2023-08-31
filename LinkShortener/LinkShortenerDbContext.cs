using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;
using LinkShortener.Services;

namespace LinkShortener
{
    public class LinkShortenerDbContext : DbContext
    {
        public LinkShortenerDbContext(DbContextOptions<LinkShortenerDbContext> options) : base(options) { }
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.ShortUrlCode).HasMaxLength(UrlShortener.NumberOfCharsShortLink);
                builder.HasIndex(s => s.ShortUrlCode).IsUnique();
            });
        }
    }
}
