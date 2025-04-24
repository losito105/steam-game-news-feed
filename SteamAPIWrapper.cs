namespace steamgamenewsfeed;

using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Collections.Generic;
using LanguageDetection;
using System.Globalization;

public static class SteamAPIWrapper
{
	/// <summary>
	/// ISteamNews API GetNewsForApp endpoint wrapper.
	/// </summary>
	/// <param name="games">App IDs of games on Steam to fetch news articles about.</param>
	/// <param name="count">How many news articles to fetch.</param>
	/// <returns></returns>
	public static async Task<DataTable> GetNewsForApp(List<int> games, int count)
	{
        DataTable dataTable = new DataTable("Feed");

        DataColumn titleColumn = new DataColumn();
        titleColumn.ColumnName = "Title";
        titleColumn.DataType = System.Type.GetType("System.String");
        dataTable.Columns.Add(titleColumn);

        DataColumn authorColumn = new DataColumn();
        authorColumn.ColumnName = "Author";
        authorColumn.DataType = System.Type.GetType("System.String");
        dataTable.Columns.Add(authorColumn);

        DataColumn datePublishedColumn = new DataColumn();
        datePublishedColumn.ColumnName = "Date Published";
        datePublishedColumn.DataType = System.Type.GetType("System.String");
        dataTable.Columns.Add(datePublishedColumn);

        DataColumn linkColumn = new DataColumn();
        linkColumn.ColumnName = "Link";
        linkColumn.DataType = System.Type.GetType("System.String");
        dataTable.Columns.Add(linkColumn);

		foreach (int game in games)
		{
            // Fetch news articles.
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid={game}&count={count}&format=json");
            GetNewsForAppResponse getNewsForAppResponse = JsonConvert.DeserializeObject<GetNewsForAppResponse>(await response.Content.ReadAsStringAsync());

            // Fill the table with the real game news data.
            foreach (NewsItem newsItem in getNewsForAppResponse.AppNews.NewsItems)
            {
                // Filter out articles that are not in English, include web-scraped HTML, or that are older than one (long) month.
                LanguageDetector detector = new LanguageDetector();
                detector.AddLanguages("en");

                DateTimeOffset datePublished = DateTimeOffset.FromUnixTimeSeconds(newsItem.Date);
                if (detector.Detect(newsItem.Title) == "en" 
                    && detector.Detect(newsItem.Author) == "en" 
                    && datePublished > DateTimeOffset.Now.Subtract(TimeSpan.FromDays(31)))
                {
                    DataRow row = dataTable.NewRow();
                    row["Title"] = newsItem.Title;
                    row["Author"] = newsItem.Author;
                    row["Date Published"] = datePublished.ToString("f");
                    row["Link"] = newsItem.Url;
                    dataTable.Rows.Add(row);
                }
            }
        }

		return dataTable;
	}
}