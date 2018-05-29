using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;
using SchoolDashboard.DAL;
using SchoolDashboard.Common;

namespace SchoolDashboard.Controllers.Tiles
{
    class TimelineTile : Tile
    {
        public override bool IsActive { get { return Repository.GetCalendarEvents(1).Length > 0; } }

        public override string TileId { get { return "timelinePanel"; } }

        public override DataModel GetViewData()
        {
            var events = Repository.GetCalendarEvents(7)
                .Select(e => new TimelineItem()
                {
                    Day = e.DateTime.Day.ToString(),
                    Description = e.Description,
                    Mounth = Helpers.MonthToLocalName(e.DateTime.Month),
                    Place = e.Place,
                    Time = e.DateTime.ToShortTimeString(),
                    HasTime = e.HasTime
                }).ToArray();

            return new TimelineTileData() { TimelineItems = events };
        }
    }
}
