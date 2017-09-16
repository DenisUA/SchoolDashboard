using SchoolDashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Controllers.Tiles.ViewDataModels
{
    class NoticesTileData : DataModel
    {
        public override int ShowTime
        {
            get { return 10000; }
        }

        public Notice[] Notices { get; set; }
    }
}
