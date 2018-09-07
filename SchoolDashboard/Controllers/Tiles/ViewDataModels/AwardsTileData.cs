using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Controllers.Tiles.ViewDataModels
{
    class AwardsTileData : DataModel
    {
        public override int ShowTime { get { return 15000; } }

        public Award[] Awards { get; set; }
    }

    class Award
    {
        public string ImageName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
