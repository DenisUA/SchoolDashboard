using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web.Models
{
    public class Holiday : DAL.Models.Holiday
    {
        public Holiday()
        {
        }

        public Holiday(DAL.Models.Holiday holiday)
        {
            Id = holiday.Id;
            Day = holiday.Day;
            Month = holiday.Month;
            Name = holiday.Name;
            Picture = holiday.Picture;
            Description = holiday.Description;
        }

        public string DateString {
            get
            {
                return Date.ToString("MM/dd");
            }
        }
    }
}
