using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;
using SchoolDashboard.Common;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Threading;
using System.IO;
using SchoolDashboard.DAL;

namespace SchoolDashboard.Controllers.Tiles
{
    class FamousBirthdaysTile : Tile
    {
        public override bool IsActive { get { return false; } }

        public override string TileId { get { return ""; } }

        public override DataModel GetViewData()
        {
            var now = DateTime.Now;
            for (int i = 0; i < 365; i++)
            {
                Console.WriteLine("Processing day: " + i);
                GetBirthdays(now.AddDays(i));
            }
            
            return null;
        }

        private void GetBirthdays(DateTime date)
        {
            var url = "https://uk.wikipedia.org/wiki/";            
            url += date.Day + "_";
            url += Uri.EscapeDataString(Helpers.MonthToLocalName(date.Month).ToLower());

            var webClient = new WebClient();
            
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("//*[@class=\"mw-parser-output\"]").First().ChildNodes;

            var divs = nodes.Where(n => n.Name == "div" && n.Attributes["class"].Value == "thumb tright").ToArray();
            foreach (var div in divs)
                div.Remove();

            var texts = nodes.Where(n => n.Name == "#text" && n.InnerText == "\n").ToArray();
            foreach (var text in texts)
                text.Remove();

            var title = nodes.Where(n => (n.Name == "h2" || n.Name == "h3") && (n.InnerText.Contains("Народились") || n.InnerText.Contains("Народилися"))).First();
            HtmlNode ulNode = title.NextSibling;
            while(true)
            {
                if (ulNode.Name != "ul")
                    ulNode = ulNode.NextSibling;
                else
                    break;
            }

            var regex1 = new Regex(@"\d{3,4}(\s|(\&\#160\;)|(&nbsp;))\W\s(?<name>[^,]+)\,");
            var regex = new Regex(@"\d+");
            
            var peoples = new List<Tuple<string, string, string>>();
            foreach (var li in ulNode.Elements("li"))
            {
                var res = ProcessLi(li, regex, regex1);
                if (res != null)
                    peoples.Add(res);
            }
            if (ulNode.NextSibling.Name == "ul")
            {
                foreach (var li in ulNode.NextSibling.Elements("li"))
                {
                    var res = ProcessLi(li, regex, regex1);
                    if (res != null)
                        peoples.Add(res);
                }
            }

            peoples.Reverse();
            var i = 0;
            foreach (var people in peoples.Take(10))
            {
                var photoFileName =  date.Month + "-" + date.Day + "-" + i++ + Path.GetExtension(people.Item3);
                webClient.DownloadFile(people.Item3, Path.Combine(@"C:\g\photos", photoFileName));
                Repository.AddFamousBirthday(new DAL.Models.FamousBirthday(date.Day, date.Month, people.Item1, people.Item2, photoFileName));
            }

            
        }

        private Tuple<string, string, string> ProcessLi(HtmlNode li, Regex regex, Regex regex1)
        {
            if (li.InnerText.Contains("порно"))
                return null;

            var match = regex1.Match(li.InnerText);
            if (!match.Success)
                return null;

            var link = li.ChildNodes.Where(n => n.Name == "a" && n.InnerText.ToLower() == match.Groups["name"].Value.ToLower()).FirstOrDefault();
            if (link == null)
                return null;

            var info = GetPhotoAndDescription(link.Attributes["href"].Value);
            if (info == null)
                return null;

            return new Tuple<string, string, string>(match.Groups["name"].Value, info.Item1, info.Item2);
        }

        private Tuple<string, string> GetPhotoAndDescription(string link)
        {
            if (link.Contains("action=edit") || !link.StartsWith("/wiki/"))
                return null;

            var doc = (new HtmlWeb()).Load("https://uk.wikipedia.org" + link);

            var descriptionNode = doc.DocumentNode.SelectNodes("//div[@class=\"mw-parser-output\"]").First().ChildNodes.Where(n => n.Name == "p").FirstOrDefault();
            if (descriptionNode == null)
                return null;

            var descriptionText = descriptionNode.InnerText;
            descriptionText = descriptionText.Replace("&#160;", "").Replace("&nbsp;", "");

            var infoTable = doc.DocumentNode.SelectSingleNode("//table[@class=\"infobox\"]");
            if (infoTable == null)
                return null;

            var img = infoTable.SelectSingleNode(".//img[@width>100 and @height>100]");
            if (img == null)
                return null;

            var urlAttr = img.Attributes["src"];
            if (urlAttr == null)
                return null;

            var imgUrl = urlAttr.Value.StartsWith("//") ? "https:" + urlAttr.Value : urlAttr.Value;

            return new Tuple<string,string>(descriptionText, imgUrl);
        }
    }
}
