using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CheckingBrokenLinks
{
    class Program
    {
        private static void CheckAllLinks(string mainLink, ref List<string> validLinks, ref List<string> invalidLinks, ref List<string> allLinks)
        {
            CheckGetAllLinks(mainLink, mainLink, ref allLinks);
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

        private static void CheckGetAllLinks(string mainLink, string link, ref List<string> allLinks)
        {
            try
            {
                GetAllLinks(mainLink, link, ref allLinks);
            }
            catch (System.ArgumentNullException)
            {
                return;
            }
        }

        private static void GetAllLinks(string mainLink, string link, ref List<string> allLinks)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(link);
            HtmlNode[] htmlNodes = htmlDocument.DocumentNode.SelectNodes("//a").ToArray();

            foreach (var node in htmlNodes)
            {
                string url = node.GetAttributeValue("href", null);
                if (!allLinks.Contains(url) && Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    if (url.Contains(mainLink) || (!url.StartsWith("http://") && !url.StartsWith("https://")))
                    {
                        allLinks.Add(url);
                        Console.WriteLine(url);

                        if (url.Contains(mainLink))
                            CheckGetAllLinks(mainLink, url, ref allLinks);
                        else
                            CheckGetAllLinks(mainLink, mainLink + url, ref allLinks);
                    }
                }
            }
        }

        private static string CheckNumberOfArguments(string[] args)
        {
            if (args.Length != 1)
                throw new Exception("Invalid number of arguments");

            return args[0];
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
                CheckAllLinks(CheckNumberOfArguments(args), ref validLinks, ref invalidLinks, ref allLinks);

                using (StreamWriter validFile = new StreamWriter("valid.txt"))
                {
                    using (StreamWriter invalidFile = new StreamWriter("invalid.txt"))
                    {
                        WriteLinks(validFile, validLinks);
                        WriteLinks(invalidFile, invalidLinks);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
