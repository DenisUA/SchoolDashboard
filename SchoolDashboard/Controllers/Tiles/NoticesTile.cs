using SchoolDashboard.Controllers.Tiles.ViewDataModels;
using SchoolDashboard.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Controllers.Tiles
{
    class NoticesTile : Tile
    {
        public override bool IsActive
        {
            get { return Repository.GetNotices(1).Length > 0; }
        }

        public override string TileId
        {
            get { return "noticesPanel"; }
        }

        public override ViewDataModels.DataModel GetViewData()
        {
            var notices = Repository.GetNotices(9);
            return new NoticesTileData() { Notices = notices };
        }
    }
}
