using SchoolDashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Common
{
    static class Helpers
    {
        public static string MonthToLocalName(int mounth)
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

        public static string AwardTypeToImageName(AwardsTypes type)
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

        public static Dictionary<AwardsTypes, string> GetAllAwards()
        {
            return new Dictionary<AwardsTypes, string>
            {
                { AwardsTypes.Basketball, "basketboll.png" },
                { AwardsTypes.Crown,"crown.png"},
                { AwardsTypes.Diploma,"diploma.png"},
                { AwardsTypes.Football,"football.png"},
                { AwardsTypes.Medal,"medal.png"},
                { AwardsTypes.Medal1,"medal3.png"},
                { AwardsTypes.Medal2,"medal4.png"},
                { AwardsTypes.Medal3,"medal6.png"},
                { AwardsTypes.Shield,"shiled.png"},
                { AwardsTypes.Star,"star.png"},
                { AwardsTypes.Star1,"star2.png"},
                { AwardsTypes.Trophy,"trophy.png"},
                { AwardsTypes.Trophy1,"trophy2.png"},
                { AwardsTypes.Voleyboll,"voleyboll.png"},
            };
        }
    }
}
