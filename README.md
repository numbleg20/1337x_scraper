1337x_scraper


install nuget package - Html Agility Pack

# Using 1337x

**To get items from the search:**
```C#
var torrents = await _1337x.GetAllFromSearch(/*request text*/);
foreach (var torrent in torrents) {
  Console.WriteLine("Name: " + torrent.Name);
  Console.WriteLine("Uploader: " + torrent.Uploader);
  Console.WriteLine("Link: " + torrent.Link);
  Console.WriteLine("Size: " + torrent.Size);
  Console.WriteLine("Seeds: " + torrent.Seeds);
  Console.WriteLine("Leeches: " + torrent.Leeches);
}
```
Output:
```
Request Text: dishonored

Name: Dishonored 2-STEAMPUNKS
Uploader: MisterBone
Link: https://1337xx.to//torrent/2247610/Dishonored-2-STEAMPUNKS/
Size: 35.2 GB
Seeds: 146
Leeches: 70
```
<br>

**To extract torrent information from a link to your page:**
```C#
var info = await _1337x.GetInfoFromLink(/*torrent page link*/);
foreach (var inf in info) {
  Console.WriteLine("Link Info:");
  Console.WriteLine("Infohash: " + inf.Infohash);
  Console.WriteLine("Download Url: " + inf.DownloadUrl);
  Console.WriteLine("Downloads: " + inf.Downloads);
  Console.WriteLine("Language: " + inf.Language);
  Console.WriteLine("");
}
```

Output:
```
Torrent Page Link: https://1337xx.to//torrent/2247610/Dishonored-2-STEAMPUNKS/

Link Info:
Infohash: C73B061A1DFC31DAF8A1E6E412BE7F03A564CD3D
Download Url: magnet:?xt=urn:btih:C73B061A1DFC31DAF8A1E6E412BE7F03A564CD3D&dn=Dishonored+2-STEAMPUNKS...
Downloads: 10352
Language: English
```

# Using gog

**To get items from the search:**
```C#
var torrents = await gog.GetAllFromSearch(/*request text*/);
Console.WriteLine("");

foreach ( var torrent in torrents ) {
  Console.WriteLine("Name: " + torrent.Name);
  Console.WriteLine("Link: " + torrent.Link);
  Console.WriteLine("");
}
```
Output:
```
Request Text: dishonored

Name: Dishonored: Complete Collection (Latest)
Link: https://freegogpcgames.com/4547/4-dishonored-complete-collection-free-download/
```
<br>

**To extract torrent information from a link to your page:**
```C#
var info = await gog.GetInfoFromLink(/*torrent page link*/);
foreach(var inf in info) {
  Console.WriteLine("Torrent Info:");
  Console.WriteLine("Name: " + inf.Name);
  Console.WriteLine("Image Link: " + inf.ImageLink);
  Console.WriteLine("Download Link: " + inf.DownloadLink);
  Console.WriteLine("Size: " + inf.Size);
  Console.WriteLine("");
}
```

Output:
```
Torrent Page Link: https://freegogpcgames.com/4547/4-dishonored-complete-collection-free-download/

Torrent Info:
Name: Dishonored: Complete Collection (Latest)
Image Link: https://i0.wp.com/uploads.freegogpcgames.com/image/Dishonored-Complete-Collection.jpg?resize=678%2C381&amp;ssl=1
Download Link: magnet:?xt=urn:btih:794E2DDF32B0979E97987EAAE19A2...
Size: 141.2 GB
```
