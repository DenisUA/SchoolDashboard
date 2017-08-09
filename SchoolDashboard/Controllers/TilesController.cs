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

        public TilesController()
        {
            _tilesSets = new Tile[][]
            {
                new Tile[] { new HistoryDayTile(), new WeatherTile() },
                new Tile[] { new TimelineTile() },
                new Tile[] { new AwardsTile() },
                new Tile[] { new BirthdaysTile() }
            };
            _currentTileSetIndex = 0;
        }

        public ShowTilesInfo[] GetShowTilesInfo()
        {
            if (_currentTileSetIndex > _tilesSets.Length - 1)
                _currentTileSetIndex = 0;

            var setIndex = _currentTileSetIndex;
            while (!_tilesSets[setIndex].Any(t => t.IsActive))
                setIndex++;

            var res = _tilesSets[setIndex]
                .Where(t => t.IsActive)
                .Select(t => new ShowTilesInfo() { TileId = t.TileId, Data = t.GetViewData() })
                .ToArray();
            _currentTileSetIndex = setIndex + 1;

            return res;
        }
    }

    class ShowTilesInfo
    {
        public string TileId { get; set; }
        public DataModel Data { get; set; }
    }
}
