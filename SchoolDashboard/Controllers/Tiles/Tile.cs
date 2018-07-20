using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;

namespace SchoolDashboard.Controllers.Tiles
{
    abstract class Tile
    {
        public abstract bool IsActive { get; }
        public abstract string TileId { get; }
        public abstract DataModel GetViewData();
        public virtual bool IsPriority { get; } = false;
    }
}
