using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

internal struct torrentInfo {
	public string DownloadUrl;
	public string Infohash;
	public string Downloads;
	public string Language;
}

internal struct torrent {
	public string Name;
	public string Uploader;
	public string Link;
	public string Size;
	public string Seeds;
	public string Leeches;
}
internal class _1337x {
	private static string _1337x_link = "https://1337xx.to";
	public static async Task<List<torrent>> GetAllFromSearch(string requestText) {
		var newList = new List<torrent>();

		requestText = requestText.Substring(0, requestText.IndexOf(' ', requestText.IndexOf(' ') + 1));

		var rutackerWindowsRequestLink = $"{_1337x_link}/category-search/{requestText}/Games/1/";
		try {
			var httpClient = new HttpClient();
			var html = await httpClient.GetStringAsync(rutackerWindowsRequestLink);
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);

			var torrents = htmlDocument.DocumentNode.SelectNodes(
					"//table[contains(@class, 'table-list table table-responsive table-striped')]/tbody/tr");

			if (torrents != null) {
				foreach (var torrentNode in torrents) {
					var torrentz = new torrent();

					var nameNode = torrentNode.SelectSingleNode(".//td[contains(@class, 'coll-1 name')]/a[2]");
					var uploaderNode = torrentNode.SelectSingleNode(".//td[contains(@class, 'coll-5 uploader')]");
					var sizeNode = torrentNode.SelectSingleNode(".//td[contains(@class, 'coll-4 size mob-uploader')]");
					var seedsNode = torrentNode.SelectSingleNode(".//td[contains(@class, 'coll-2 seeds')]");
					var leechesNode = torrentNode.SelectSingleNode(".//td[contains(@class, 'coll-3 leeches')]");

					if (nameNode != null && uploaderNode != null) {

						var link = nameNode.Attributes["href"].Value;
						var name = nameNode.InnerText.Trim();
						var uploader = uploaderNode.InnerText.Trim();
						var size = sizeNode.InnerText.Trim();
						var seeds = seedsNode.InnerText.Trim();
						var leeches = leechesNode.InnerText.Trim();

						if (!string.IsNullOrEmpty(name)) {
							if (name.ToLower().Contains(requestText.ToLower()) && int.Parse(leeches) > 0 && int.Parse(seeds) > 0) {
								torrentz.Name = name;
								torrentz.Uploader = uploader;
								torrentz.Link = $"{_1337x_link}/{link}";
								torrentz.Size = size;
								torrentz.Seeds = seeds;
								torrentz.Leeches = leeches;
								newList.Add(torrentz);
							}
						}
					}
				}
			}

		}
		catch (Exception ex) {
			Console.WriteLine("\nError: " + ex.Message);
		}
		return newList;
	}

	public static async Task<List<torrentInfo>> GetInfoFromLink(string link) {
		var newList = new List<torrentInfo>();

		try {
			var httpClient = new HttpClient();
			var html = await httpClient.GetStringAsync(link);
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);

			var torrentInfoz = new torrentInfo();
			var infohash = htmlDocument.DocumentNode.SelectSingleNode(".//div[contains(@class, 'infohash-box')]/p/span");
			var downloads = htmlDocument.DocumentNode.SelectSingleNode(".//div[contains(@class, 'clearfix')]/ul[3]/li[1]/span");
			var language = htmlDocument.DocumentNode.SelectSingleNode(".//div[contains(@class, 'clearfix')]/ul[2]/li[3]/span");
			var linkNode = htmlDocument.DocumentNode.SelectSingleNode(
				"//a[contains(@class, 'torrentdown1')]");

			torrentInfoz.DownloadUrl = linkNode.Attributes["href"].Value.Replace("&amp;", "&");
			torrentInfoz.Infohash = infohash.InnerText.Trim();
			torrentInfoz.Downloads = downloads.InnerText.Trim();
			torrentInfoz.Language = language.InnerText.Trim();

			newList.Add(torrentInfoz);

		}
		catch (Exception ex) {
			Console.WriteLine("\nError: " + ex.Message);
		}
		return newList;
	}
}
