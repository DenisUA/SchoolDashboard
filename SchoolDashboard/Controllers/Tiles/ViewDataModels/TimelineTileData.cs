using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Controllers.Tiles.ViewDataModels
{
    class TimelineTileData : DataModel
    {
        public override int ShowTime { get { return 20000; } }
        public TimelineItem[] TimelineItems { get; set; }
    }

    class TimelineItem
    {
        public string Day { get; set; }
        public string Mounth { get; set; }
        public string Time { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public bool HasTime { get; set; }
    }
}
