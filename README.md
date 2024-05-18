# UrlShortener
Url Shortening API that takes advantage of caching 302 redirect requests by overriding the .NET memory cache policy and pregenerates available urls in order to minimize user latency.

## Inspiration
I was just browsing YouTube when I found a video depicting how to make a Url Shortener as a project. When I was watching it, I realized that the creator left a lot of performance oppurtunities on the table, so I decided to implement these performance boosts on my own.

Credit to Milan Jovanovic for the original url shortener code architecture.

---

## What it does
1. It has a few main API endpoints including "/shorten" and "/{shorturlcode}", which accept HTTP requests that perform the basic functions of the application.

2. Overrides the standard .NET HTTP cache policy to allow for 302 (Redirect) responses to be cached.

3. Uses .NET Quartz (a Port of Java's Quartz library) to automaticially generate valid short links when the available pool is running low.

---

## How it's built
This project is implemented in C# using the .Net Core framework. It also uses an in-memory database to avoid needing to make expensive database calls.

---

## Challenges I ran into
Getting the new cache policy that I wrote to add and evict correctly took some time, but showed me a lot about cache coherency and the issues most applications would face by making the changes that positively affect my app.

## Accomplishments that I'm proud of / What I Learned
- Learned a lot about how automatic output cache policies work for a few different languages through my research
- Worked more with Quartz to run background tasks
- First project working in .NET Core outside of work

## What's next for the Shortener
- Frontend for user accessibility
- On-disk Database for data durabillity
- Possible orchestration to run multiple copies and then the redo of internal state to work with the new changes

---

## Setup and Execution
### Running locally
```bash
cd LinkShortener

# Make sure you have the .Net 7 SDK installed

dotnet run
```

### Running on Docker
```bash
# make sure to be in the outer directory
docker build -f LinkShortener\Dockerfile -t link-shortener .

docker run link-shortener
```
## API usage
The api currently has 3 endpoints:
1. "/shorten" POST
```javascript
{
  "url": "https://foo.com/bar"
}
```
2. "/{shortenedurl}" GET
3. "/urls" GET

