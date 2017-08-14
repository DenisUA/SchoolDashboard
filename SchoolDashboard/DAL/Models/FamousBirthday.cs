using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    class FamousBirthday
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        public FamousBirthday(int day, int month, string name, string description, string photo)
        {
            Day = day;
            Month = month;
            Name = name;
            Description = description;
            Photo = photo;
        }
    }
}
