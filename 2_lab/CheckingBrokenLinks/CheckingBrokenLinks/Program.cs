using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//statler.ru
//koptelnya.ru

namespace CheckingBrokenLinks
{
    class Program
    {
        private static void GettingStatusCodes(string mainLink, ref List<string> validLinks, ref List<string> invalidLinks, ref List<string> allLinks, StreamWriter stream)
        {
            CheckGetLinks(mainLink, mainLink, ref allLinks, stream);
            allLinks.Add(mainLink);

            HttpClient client = new HttpClient();
            string url, answer;

            foreach (var link in allLinks)
            {
                url = link;
                if (!link.Contains(mainLink))
                    url = mainLink + link;

                using (var response = client.GetAsync(url).Result)
                {
                    int code = (int)response.StatusCode;
                    answer = $"{url} {code} {response.StatusCode}";

                    if (code >= 200 && code < 400)
                        validLinks.Add(answer);
                    else
                        invalidLinks.Add(answer);
                }
            }
        }

        private static void CheckGetLinks(string mainLink, string link, ref List<string> allLinks, StreamWriter stream)
        {
            try
            {
                GetLinks(mainLink, link, ref allLinks, stream);
            }
            catch (System.ArgumentNullException)
            {
                return;
            }
        }

        private static bool CheckHref(string href)
        {
            return (!href.StartsWith("http://") && !href.StartsWith("https://") && (href != "/") &&
                !href.Contains("tel:") && !href.Contains("mailto:") && !href.Contains("tg:")) &&
                !href.Contains("javascript:") && !href.Contains("viber:") ? true : false;
        }

        private static void GetLinks(string mainLink, string link, ref List<string> allLinks, StreamWriter stream)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(link);
            HtmlNode[] htmlNodes = htmlDocument.DocumentNode.SelectNodes("//a").ToArray();

            foreach (var node in htmlNodes)
            {
                string url = node.GetAttributeValue("href", null);
                if (url != null)
                    url = url.Replace(" ", "");
                if (!allLinks.Contains(url) && Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    if (url.Contains(mainLink) || CheckHref(url))
                    {
                        stream.WriteLine($"Parent: {link}");
                        stream.WriteLine($"Child: {url}");

                        allLinks.Add(url);

                        if (url.Contains(mainLink))
                            CheckGetLinks(link, url, ref allLinks, stream);
                        else
                            CheckGetLinks(mainLink, mainLink + url, ref allLinks, stream);
                    }
                }
            }
        }

        private static void WriteLinks(StreamWriter stream, List<string> links)
        {
            foreach (var link in links)
                stream.WriteLine(link);

            stream.WriteLine("Number of links: " + links.Count());
            stream.WriteLine("Date of verification: " + DateTime.Now);
        }


        static void Main(string[] args)
        {
            List<string> validLinks = new List<string>();
            List<string> invalidLinks = new List<string>();
            List<string> allLinks = new List<string>();

            try
            {
                if (args.Length == 1)
                    using (StreamWriter usefulFile = new StreamWriter("../../../useful_information_about_links.txt"))
                    {
                        GettingStatusCodes(args[0], ref validLinks, ref invalidLinks, ref allLinks, usefulFile);

                        using (StreamWriter validFile = new StreamWriter("../../../valid.txt"))
                        {
                            using (StreamWriter invalidFile = new StreamWriter("../../../invalid.txt"))
                            {
                                WriteLinks(validFile, validLinks);
                                WriteLinks(invalidFile, invalidLinks);
                            }
                        }
                    }
                else throw new Exception("Invalid number of arguments");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
