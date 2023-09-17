using DataScraper;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using System.Web;

internal class Program
{
    private static void Main(string[] args)
    {
        while (!GetData())
        {

        }

        static bool GetData()
        {
            try
            {
                var client = new WebClient();
                var noProxyClient = new WebClient();
                DataScrapContext context = new DataScrapContext();
                //client.Proxy = new WebProxy("p.webshare.io:80");
                //client.Proxy.Credentials =
                //  new NetworkCredential("gunmiiff-rotate", "hsy8t7x155oo");
                var response = client.DownloadString("https://1337x.to/trending/w/movies/");
                if (response.Contains("Trending MOVIES Torrents last 7 days"))
                {
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(response);
                    var tr = document.DocumentNode.Descendants("tbody").First().Descendants("tr").ToList();
                    tr.RemoveRange(0, 27);
                    foreach (var entry in tr)
                    {
                        var url = "https://1337x.to" + entry.Descendants("a").First().Attributes.First().Value;
                        Console.WriteLine($"Fetching Data from : {url}");
                        var pageData = client.DownloadString(url);
                        HtmlDocument torrentData = new HtmlDocument();
                        torrentData.LoadHtml(pageData);

                        var movieMeta = new moviedata();
                        if (torrentData.DocumentNode.Descendants("h3").First().Descendants().First().HasAttributes)
                        {
                            movieMeta.id = torrentData.DocumentNode.Descendants("h3").First().Descendants().First().Attributes.First().Value;
                            int lastIndex = movieMeta.id.LastIndexOf('/');
                            int secondLastIndex = movieMeta.id.LastIndexOf('/', lastIndex - 1);
                            if (secondLastIndex >= 0)
                            {
                                movieMeta.id = movieMeta.id.Substring(0, secondLastIndex + 1);
                            }
                            else
                            {
                                movieMeta.id = movieMeta.id.Substring(0, lastIndex + 1);
                            }
                            movieMeta.id = movieMeta.id.Split('/')[2];
                            if (!context.moviedata.Any(n => n.id == movieMeta.id))
                            {
                                var tmdbLink = "https://www.themoviedb.org/movie/" + movieMeta.id;
                                var tmdbData = noProxyClient.DownloadString(tmdbLink);
                                HtmlDocument tmdbpageData = new HtmlDocument();
                                tmdbpageData.LoadHtml(tmdbData);
                                var mDetail = tmdbpageData.DocumentNode.Descendants("section").Where(n => n.Id == "original_header").First();
                                movieMeta.imgurl = "https://www.themoviedb.org" + mDetail.Descendants("img").First().Attributes.Where(i => i.Name == "data-srcset").First().Value.Split(" ")[2];
                                movieMeta.score = mDetail.Descendants("div").Where(n => n.Attributes.Count == 4).First().GetAttributeValue("data-percent", "0").Split('.')[0];
                                movieMeta.name = mDetail.Descendants("h2").First().Descendants("a").First().InnerText;
                                if (mDetail.Descendants("span").Where(n => n.OuterHtml.Contains("runtime")).Count() > 0)
                                {
                                    var hours = mDetail.Descendants("span").Where(n => n.OuterHtml.Contains("runtime")).First().InnerText.Trim().Split()[0];
                                    var mins = "0m";
                                    if (mDetail.Descendants("span").Where(n => n.OuterHtml.Contains("runtime")).First().InnerText.Trim().Split().Length > 1)
                                    {
                                        mins = mDetail.Descendants("span").Where(n => n.OuterHtml.Contains("runtime")).First().InnerText.Trim().Split()[1];
                                    }
                                    movieMeta.time = (Int32.Parse(hours.Remove(hours.Length - 1)) * 60 + Int32.Parse(mins.Remove(mins.Length - 1))).ToString();
                                }
                                else
                                {
                                    movieMeta.time = "0";
                                }
                                Console.WriteLine($"Fetched {movieMeta.name} Details.");
                                Console.WriteLine($"Inserting {movieMeta.name} to Database.");
                                context.moviedata.Add(movieMeta);
                                var tid = context.SaveChanges().ToString();
                            }
                            else
                            {
                                Console.WriteLine($"Skipped {movieMeta.name} as already exists.");
                            }
                            var movieid = "https://1337x.to" + torrentData.DocumentNode.Descendants("h3").First().Descendants().First().Attributes.First().Value;
                            var movielistpage = client.DownloadString(movieid);
                            var movielist = new HtmlDocument();
                            movielist.LoadHtml(movielistpage);
                            var mList = movielist.DocumentNode.Descendants("tr").ToList();
                            mList.RemoveAt(0);
                            var i = 0;
                            foreach (var movie in mList)
                            {
                                Console.WriteLine($"Fetching {movieMeta.name} Link {++i}.");
                                var murl = "https://1337x.to" + movie.Descendants("a").ElementAt(1).Attributes.First().Value;
                                var movieData = client.DownloadString(murl);
                                var movieHtml = new HtmlDocument();
                                movieHtml.LoadHtml(movieData);
                                var link = GetLinkData.Get(movieHtml, movieMeta.id);
                                if (!context.movielink.Any(n => n.magnet == link.magnet))
                                {
                                    Console.WriteLine($"Inserting {movieMeta.name} Link {i} to Database.");
                                    context.movielink.Add(link);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine($"{movieMeta.name} Link exist, SKIPPING.");
                                }
                            }
                            Console.WriteLine($"Done");
                            Console.WriteLine("\n");
                            Console.WriteLine("\n");
                            Console.WriteLine("\n");
                            Console.WriteLine("\n");
                        }
                        else
                        {
                            Console.WriteLine($"Skipped as movie doesnot exist in TMDB.");
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }
}