using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;
using SchoolDashboard.DAL;

namespace SchoolDashboard.Controllers.Tiles
{
    class HistoryDayTile : Tile
    {
        public override bool IsActive { get { return true; } }

        public override string TileId { get { return "historyDayPanel"; } }

        public override DataModel GetViewData()
        {
            var today = DateTime.Now;
            var holiday = Repository.GetHoliday(today.Month, today.Day);
            if (holiday == null)
            {
                var fact = Repository.GetRandomFact();
                return new HistoryDayTileData()
                {
                    Title = "А ви знали що...",
                    Text = fact,
                    Image = "/Images/question.jpeg"
                };
            }
            else
            {
                return new HistoryDayTileData()
                {
                    Title = holiday.Name,
                    Text = holiday.Description,
                    Image = "/Images/HolidaysPictures/" + holiday.Picture
                };
            }
        }
    }
}
