using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;
using SchoolDashboard.Common;
using System.Net;
using HtmlAgilityPack;

namespace SchoolDashboard.Controllers.Tiles
{
    class FamousBirthdaysTile : Tile
    {
        public override bool IsActive { get { return false; } }

        public override string TileId { get { return ""; } }

        public override DataModel GetViewData()
        {
            var url = "https://uk.wikipedia.org/wiki/";
            var now = DateTime.Now;
            url += now.Day + "_";
            url += Uri.EscapeDataString(Helpers.MonthToLocalName(now.Month).ToLower());

            var web = new HtmlWeb();
            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("//*[@id=\"mw-content-text\"]/div/ul[2]");

            return null;
        }
    }
}
