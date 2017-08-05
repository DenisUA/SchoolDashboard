using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    class Achievement
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public AchivmentTypes Type { get; set; }
    }

    enum AchivmentTypes
    {
        FirstPlace = 0,
        SecondPlace = 1,
        ThirdPlace = 2,
        Diploma = 3,
        Medal = 4,
        Footbal = 5,
        Cup = 6
    }
}
