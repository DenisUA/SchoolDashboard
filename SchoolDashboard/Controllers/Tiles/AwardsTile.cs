using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;
using SchoolDashboard.DAL;
using SchoolDashboard.DAL.Models;

namespace SchoolDashboard.Controllers.Tiles
{
    class AwardsTile : Tile
    {
        public override bool IsActive { get { return true; } }

        public override string TileId { get { return "awardsPanel"; } }

        public override DataModel GetViewData()
        {
            var awards = Repository.GetAwards(8);
            var res = awards.Select(a => new Award() { Title = a.Owner, Description = a.Description, ImageName = AwardTypeToImageName(a.Type) });
            return new AwardsTileData() { Awards = res.ToArray()};
        }

        private string AwardTypeToImageName(AwardsTypes type)
        {
            switch (type)
            {
                case AwardsTypes.Basketball:
                    return "basketboll.png";
                case AwardsTypes.Crown:
                    return "crown.png";
                case AwardsTypes.Diploma:
                    return "diploma.png";
                case AwardsTypes.Football:
                    return "football.png";
                case AwardsTypes.Medal:
                    return "medal.png";
                case AwardsTypes.Medal1:
                    return "medal3.png";
                case AwardsTypes.Medal2:
                    return "medal4.png";
                case AwardsTypes.Medal3:
                    return "medal6.png";
                case AwardsTypes.Shield:
                    return "shiled.png";
                case AwardsTypes.Star:
                    return "star.png";
                case AwardsTypes.Star1:
                    return "star2.png";
                case AwardsTypes.Trophy:
                    return "trophy.png";
                case AwardsTypes.Trophy1:
                    return "trophy2.png";
                case AwardsTypes.Voleyboll:
                    return "voleyboll.png";
                default:
                    return "";
            }
        }
    }
}
