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
        public override bool IsActive { get; }

        public override string TileId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DataModel GetViewData()
        {
            throw new NotImplementedException();
        }
    }
}
