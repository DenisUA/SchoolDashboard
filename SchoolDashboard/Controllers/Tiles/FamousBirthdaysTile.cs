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
                GetBirthdays(now.AddDays(i));
            }

            return null;
        }

        private void GetBirthdays(DateTime date)
        {
            var url = "https://uk.wikipedia.org/wiki/";            
            url += date.Day + "_";
            url += Uri.EscapeDataString(Helpers.MonthToLocalName(date.Month).ToLower());

            var web = new HtmlWeb();
            var doc = web.Load(url);
            var ulNodes = doc.DocumentNode.SelectNodes("//*[@class=\"mw-parser-output\"]/ul");
            var regex = new Regex(@"\d{3,4}(\s|(\&\#160\;)|(&nbsp;))\W\s[А-я]+");

            var divs = ulNodes[0].ParentNode.SelectNodes("div");
            if (divs != null)
            {
                foreach (var div in divs)
                {
                    div.Remove();
                }
            }

            Console.Write(date.Day + " " + Helpers.MonthToLocalName(date.Month) + ": ");
            var count = 0;
            foreach (var ulNode in ulNodes)
            {
                if (!ulNode.PreviousSibling.PreviousSibling.InnerText.Contains("Народились"))
                    continue;

                foreach (var li in ulNode.Elements("li"))
                {
                    if (!regex.IsMatch(li.InnerText))
                        continue;

                    //var name = li.ChildNodes[2].InnerText;
                    //Console.WriteLine(name);
                    count++;
                }
            }
            Console.WriteLine(count);
        }
    }
}
