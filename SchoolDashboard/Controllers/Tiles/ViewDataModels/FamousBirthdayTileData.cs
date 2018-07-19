using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Controllers.Tiles.ViewDataModels
{
    class FamousBirthdayTileData : DataModel
    {
        public override int ShowTime { get { return 10000; } }

        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureFileName { get; set; }
    }
}
