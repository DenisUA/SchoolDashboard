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
            
            return null;
        }
    }
}
