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
    }
}
