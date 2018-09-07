using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Controllers.Tiles.ViewDataModels
{
    class BirthdayTileData : DataModel
    {
        public override int ShowTime { get { return 15000; } }
        public BirthdayItem[] Items { get; set; }
    }

    class BirthdayItem
    {
        public string Name { get; set; }
        public bool IsMale { get; set; }
        public string Description { get; set; }
    }
}
