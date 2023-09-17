using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper
{
    public static class GetLinkData
    {
        public static movielink Get(HtmlDocument torrentData,string id)
        {
            var list = new movielink();
            list.movieid = id;
            list.fsize = torrentData.DocumentNode.Descendants("ul").Where(n => n.InnerText.Contains("Total size")).First().Descendants("span").ElementAt(3).InnerText;
            list.seed = torrentData.DocumentNode.Descendants("ul").Where(n => n.InnerText.Contains("Seeders")).First().Descendants("span").ElementAt(3).InnerText;
            list.leech = torrentData.DocumentNode.Descendants("ul").Where(n => n.InnerText.Contains("Seeders")).First().Descendants("span").ElementAt(4).InnerText;
            list.magnet = torrentData.DocumentNode.Descendants("ul").Where(n => n.InnerText.Contains("Magnet Download")).First().Descendants("a").First().Attributes.ElementAt(1).Value;
            list.language = torrentData.DocumentNode.Descendants("ul").Where(n => n.InnerText.Contains("Language")).First().Descendants("span").ElementAt(2).InnerText;
            var quality = torrentData.DocumentNode.Descendants("h1").First().InnerText;
            if (quality.Contains("1080p"))
                list.quality = "1080";
            else if (quality.Contains("720p"))
                list.quality = "720";
            else if (quality.Contains("2160p"))
                list.quality = "2160";
            else
                list.quality = "720";
            return list;
        }
    }
}
