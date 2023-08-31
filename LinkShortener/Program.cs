using LinkShortener;
using LinkShortener.Models;
using LinkShortener.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LinkShortenerDbContext>(options =>
    options.UseInMemoryDatabase("LinkShortenerDatabase"));
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
        builder.Expire(TimeSpan.FromSeconds(10)));
});


builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddScoped<UrlShortener>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("api/shorten", async (ShortenUrlRequest request,
                            UrlShortener shortener,
                            LinkShortenerDbContext context,
                            HttpContext httpContext) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _)){
        return Results.BadRequest("The Url given is invalid");
    }

    var shortUrlCode = shortener.GenerateShortUrlCode();
    var shortenedUrl = new ShortenedUrl
    {
        Id = Guid.NewGuid(),
        LongUrl = request.Url,
        ShortUrlCode = shortUrlCode,
        Created = DateTime.UtcNow
    };
    context.Add(shortenedUrl);
    await context.SaveChangesAsync();
    return Results.Ok($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{shortenedUrl.ShortUrlCode}");
});

app.MapGet("/{shortUrlCode}", async (string shortUrlCode, LinkShortenerDbContext context) =>
{
    var url = await context.ShortenedUrls.FirstOrDefaultAsync(s => s.ShortUrlCode == shortUrlCode);

    if(url is null)
    {
        return Results.BadRequest();
    }
    return Results.Redirect(url.LongUrl);
}).CacheOutput();

app.Run();