using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Controllers.Tiles.ViewDataModels
{
    class VideoTileData : DataModel
    {
        public override int ShowTime
        {
            get
            {
                return 30;
            }
        }
    }
}
