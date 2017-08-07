using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles;

namespace SchoolDashboard.Controllers
{
    class TilesController
    {
        private Tile[][] _tilesSets;
        public TilesController()
        {
            
            _tilesSets = new Tile[][]
            {
                new []{new AchievementsTile(), new AchievementsTile(), }
            };
        }

        public ShowTilesInfo GetShowTilesInfo()
        {
            return null;
        }
    }

    class ShowTilesInfo
    {
        public string[] TileId { get; set; }
        public int ShowTime { get; set; }
    }
}
