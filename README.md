# UrlShortener
Inspired by Milan Jovanovic's Url shortener. I added in additional performance optimizations to improve speed at scale including rewriting the standard cache protocol to also
include 302 responses and pregenerated urls in order to minimize database load.

# Running locally
```bash
cd LinkShortener

# Make sure you have the .Net 7 SDK installed

dotnet run
```

# Running on Docker
```bash
# make sure to be in the outer directory
docker build -f LinkShortener\Dockerfile -t link-shortener .

docker run link-shortener
```
