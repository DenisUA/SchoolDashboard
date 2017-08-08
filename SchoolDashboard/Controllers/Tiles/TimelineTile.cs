using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;
using SchoolDashboard.DAL;

namespace SchoolDashboard.Controllers.Tiles
{
    class TimelineTile : Tile
    {
        public override bool IsActive { get { return true; } }

        public override string TileId { get { return "timelinePanel"; } }

        public override DataModel GetViewData()
        {
            var events = Repository.GetCalendarEvents(4)
                .Select(e => new TimelineItem()
                {
                    Day = e.DateTime.Day.ToString(),
                    Description = e.Description,
                    Mounth = MounthToLocalName(e.DateTime.Month),
                    Place = e.Place,
                    Time = e.DateTime.ToShortTimeString(),
                    HasTime = e.HasTime
                }).ToArray();

            return new TimelineTileData() {TimelineItems = events};
        }

        private string MounthToLocalName(int mounth)
        {
            switch (mounth)
            {
                case 1:
                    return "Січня";
                case 2:
                    return "Лютого";
                case 3:
                    return "Березня";
                case 4:
                    return "Квітня";
                case 5:
                    return "Травня";
                case 6:
                    return "Червня";
                case 7:
                    return "Липня";
                case 8:
                    return "Серпня";
                case 9:
                    return "Вересня";
                case 10:
                    return "Жовтня";
                case 11:
                    return "Листопада";
                case 12:
                    return "Грудня";
                default:
                    return "";
            }
        }
    }
}
