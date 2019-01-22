using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;

namespace SchoolDashboard.Controllers.Tiles
{
    class VideoTile : Tile
    {
        public override bool IsActive
        {
            get
            {
                return true;
            }
        }

        public override string TileId
        {
            get
            {
                return "videoPanel";
            }
        }

        public override DataModel GetViewData()
        {
            return new VideoTileData();
        }
    }
}
