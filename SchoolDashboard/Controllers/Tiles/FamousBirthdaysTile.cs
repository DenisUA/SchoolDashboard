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
        public override bool IsActive
        {
            get
            {
                var today = DateTime.Now;
                return Repository.GetFamousBirthdaysByDay(today.Month, today.Day).Length > 0;
            }
        }

        public override string TileId { get { return "famousBirthday"; } }

        public override DataModel GetViewData()
        {
            var today = DateTime.Now;
            var famousBirthday = Repository.GetFamousBirthdaysByDay(today.Month, today.Day).Random();
            var model = new FamousBirthdayTileData()
            {
                Name = famousBirthday.Name,
                Description = famousBirthday.Description,
                PictureFileName = famousBirthday.Photo
            };

            return model;
        }
    }
}
