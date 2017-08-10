using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Controllers.Tiles.ViewDataModels
{
    class HistoryDayTileData : DataModel
    {
        public override int ShowTime { get { return 10000; } }

        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}
