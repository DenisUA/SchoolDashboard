using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;

namespace SchoolDashboard.Controllers.Tiles
{
    class BirthdaysTile : Tile
    {
        public override bool IsActive { get { return true; } }

        public override string TileId { get { return "birthdaysPanel"; } }

        public override DataModel GetViewData()
        {
            var res = new BirthdayTileData();
            res.Items = new BirthdayItem[] {
                new BirthdayItem() {Name = "Лемішка Максим", PhotoName = "people1.jpg" },
                new BirthdayItem() {Name = "Сташків Анастасія", PhotoName = "people.jpg" },
                new BirthdayItem() {Name = "Дублянко Марта", PhotoName = "people2.png" },
                new BirthdayItem() {Name = "Грицина Ольга", PhotoName = "people3.jpg" },
                new BirthdayItem() {Name = "Масна Наталія", PhotoName = "people4.jpg" },
            };

            return res;
        }
    }
}
