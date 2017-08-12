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

namespace SchoolDashboard.Controllers.Tiles
{
    class FamousBirthdaysTile : Tile
    {
        public override bool IsActive { get { return false; } }

        public override string TileId { get { return ""; } }

        public override DataModel GetViewData()
        {
            var now = DateTime.Now;
            var file = new StreamWriter(@"C:\g\test.txt", false);
            for (int i = 0; i < 365; i++)
            {
                Console.WriteLine("Processing day: " + i);
                GetBirthdays(now.AddDays(i), file);
            }
            file.Close();

            return null;
        }

        private void GetBirthdays(DateTime date, StreamWriter file)
        {
            var url = "https://uk.wikipedia.org/wiki/";            
            url += date.Day + "_";
            url += Uri.EscapeDataString(Helpers.MonthToLocalName(date.Month).ToLower());
            
            
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
            file.WriteLine(date.Day + " " + Helpers.MonthToLocalName(date.Month) + "");
            var peoples = new List<Tuple<string, string>>();
            foreach (var li in ulNode.Elements("li"))
            {
                var res = ProcessLi(li, regex, regex1, file);
                if (res != null)
                    peoples.Add(res);
            }
            if (ulNode.NextSibling.Name == "ul")
            {
                foreach (var li in ulNode.NextSibling.Elements("li"))
                {
                    var res = ProcessLi(li, regex, regex1, file);
                    if (res != null)
                        peoples.Add(res);
                }
            }

            peoples.Reverse();
            foreach (var people in peoples.Take(10))
            {
                file.WriteLine(people.Item1 + " - " + people.Item2);
            }

            file.WriteLine();
        }

        private Tuple<string, string> ProcessLi(HtmlNode li, Regex regex, Regex regex1, StreamWriter file)
        {
            if (li.InnerText.Contains("порно"))
                return null;

            var match = regex1.Match(li.InnerText);
            if (!match.Success)
                return null;

            var link = li.ChildNodes.Where(n => n.Name == "a" && n.InnerText.ToLower() == match.Groups["name"].Value.ToLower()).FirstOrDefault();
            if (link == null || link.InnerText.Contains("action=edit"))
                return null;

            return new Tuple<string, string>(match.Groups["name"].Value, link.Attributes["href"].Value);
        }
    }
}
