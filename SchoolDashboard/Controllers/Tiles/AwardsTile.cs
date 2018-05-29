using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;
using SchoolDashboard.DAL;
using SchoolDashboard.DAL.Models;
using SchoolDashboard.Common;

namespace SchoolDashboard.Controllers.Tiles
{
    class AwardsTile : Tile
    {
        public override bool IsActive { get { return true; } }

        public override string TileId { get { return "awardsPanel"; } }

        public override DataModel GetViewData()
        {
            var awards = Repository.GetAwards(10);
            var res = awards.Select(a => new Award() { Title = a.Owner, Description = a.Description, ImageName = Helpers.AwardTypeToImageName(a.Type) });
            return new AwardsTileData() { Awards = res.ToArray()};
        }
    }
}
