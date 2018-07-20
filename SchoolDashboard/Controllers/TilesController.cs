using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;

namespace SchoolDashboard.Controllers
{
    class TilesController
    {
        private Tile[][] _tilesSets;
        private int _currentTileSetIndex;

        public Tile FixedTile { get; set; }
        public Tile[] Tiles { get { return _tilesSets.SelectMany(a => a.ToArray()).ToArray(); } }

        public TilesController()
        {
            _tilesSets = new Tile[][]
            {
                new Tile[] { new HistoryDayTile(), new WeatherTile() },
                new Tile[] { new TimelineTile() },
                new Tile[] { new AwardsTile() },
                new Tile[] { new BirthdaysTile(), new FamousBirthdaysTile() },
                new Tile[] { new NoticesTile() }
            };
            _currentTileSetIndex = -1;

            FixedTile = null;
        }

        public ShowTilesInfo[] GetShowTilesInfo()
        {
            if (FixedTile != null)
                return new ShowTilesInfo[] { new ShowTilesInfo() { TileId = FixedTile.TileId, Data = FixedTile.GetViewData() } };

            var newTilesSetIndex = _currentTileSetIndex + 1;
            if (newTilesSetIndex > _tilesSets.Length - 1)
                newTilesSetIndex = 0;

            while (true)
            {
                var tiles = _tilesSets[newTilesSetIndex];
                var mainTile = tiles.FirstOrDefault(t => t.IsPriority);
                if (mainTile == null)
                {
                    if (tiles.Any(t => t.IsActive))
                        break;
                }
                else
                {
                    if (mainTile.IsActive)
                        break;
                }

                newTilesSetIndex++;
                if (newTilesSetIndex > _tilesSets.Length - 1)
                    newTilesSetIndex = 0;
            }

            var res = _tilesSets[newTilesSetIndex]
                .Where(t => t.IsActive)
                .Select(t => new ShowTilesInfo() { TileId = t.TileId, Data = t.GetViewData() })
                .ToArray();

            _currentTileSetIndex = newTilesSetIndex;
            return res;
        }
    }

    class ShowTilesInfo
    {
        public string TileId { get; set; }
        public DataModel Data { get; set; }
    }
}
