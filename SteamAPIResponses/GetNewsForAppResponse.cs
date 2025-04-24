namespace steamgamenewsfeed;

using Newtonsoft.Json;

// Our added wrapper for the outermost object.
public class GetNewsForAppResponse
{
    [JsonProperty("appnews")]
    public AppNews AppNews { get; set; }
}

// Wrapper data object returned by ISteamNews API GetNewsForApp endpoint.
public class AppNews
{
    // App ID of the game on Steam.
    [JsonProperty("appid")]
    public int AppId { get; set; }

    [JsonProperty("newsitems")]
    public NewsItem[] NewsItems { get; set; }

    // Number of articles fetched.
    [JsonProperty("count")]
    public int Count { get; set; }
}

// Article metadata.
public class NewsItem
{
    // Unique identifier for Steam internal usage.
    [JsonProperty("gid")]
    public string Gid { get; set; }

    // Article Title.
    [JsonProperty("title")]
    public string Title { get; set; }

    // Link to article.
    [JsonProperty("url")]
    public string Url { get; set; }

    // NOOP
    [JsonProperty("is_external_url")]
    public string IsExternalUrl { get; set; }

    // Author of article.
    [JsonProperty("author")]
    public string Author { get; set; }

    // Short snippet of article.
    [JsonProperty("contents")]
    public string Contents { get; set; }

    // NOOP
    [JsonProperty("feedlabel")]
    public string FeedLabel { get; set; }

    // Publish date of article.
    [JsonProperty("date")]
    public long Date { get; set; }

    // NOOP
    [JsonProperty("feedname")]
    public string FeedName { get; set; }

    // NOOP
    [JsonProperty("feedtype")]
    public int FeedType { get; set; }

    // NOOP
    [JsonProperty("appid")]
    public int AppId { get; set; }
}