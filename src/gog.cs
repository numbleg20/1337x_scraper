using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

internal struct gameInfo {
	public string Name;
	public string ImageLink;
	public string DownloadLink;
}

internal struct torrent {
	public string Name;
	public string Link;
}

internal class gog {
	public static async Task<List<torrent>> GetAllFromSearch(string requestText) {
		var requestLink = $"https://freegogpcgames.com/?s={requestText}";
		var list = new List<torrent>();

		try {
			var httpClient = new HttpClient();
			var html = await httpClient.GetStringAsync(requestLink);
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);

			var torrents = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'generate-columns-container')]/article");
			if (torrents != null) {
				foreach (var torrent in torrents) {
					var name = torrent.SelectSingleNode(".//header/h2/a");
					var link = name.Attributes["href"].Value;

					if (name.InnerText.Trim().Replace(":", "").Replace("-", "").
						ToLower().Contains(requestText.Replace(":", "").Replace("-", "").ToLower())) {
						list.Add(new torrent {
							Name = name.InnerText.Trim(),
							Link = link
						});
					}
				}
			}
		}
		catch (Exception e) {
			Console.WriteLine("\nError: " + e.Message);
		}

		return list;
	}

	public static async Task<List<gameInfo>> GetInfoFromLink(string link) {
		var list = new List<gameInfo>();

		try {
			var httpClient = new HttpClient();
			var html = await httpClient.GetStringAsync(link);
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);

			var name = htmlDocument.DocumentNode.SelectSingleNode("//h1[contains(@class, 'entry-title')]");
			var imageLink = htmlDocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'featured-image')]/img").Attributes["src"].Value;
			var downloadLink = htmlDocument.DocumentNode.SelectSingleNode("//a[contains(@class, 'download-btn')]").Attributes["href"].Value;

			string bypassDownloadLink(string url) {
				var httpClientx = new HttpClient();
				var htmlx = httpClientx.GetStringAsync(url).Result;
				var htmlDocumentx = new HtmlDocument();
				htmlDocumentx.LoadHtml(htmlx);
				return htmlDocumentx.DocumentNode.SelectSingleNode("//a[contains(@class, 'button')]").Attributes["href"].Value.Replace("&amp;", "&").Replace("&#038;", "&");
			}

			list.Add(new gameInfo {
				Name = name.InnerText.Trim(),
				ImageLink = imageLink,
				DownloadLink = bypassDownloadLink(downloadLink),
			});

		}
		catch(Exception e) {
			Console.WriteLine("\nError: " + e.Message);
		}
		return list;
	}
}
