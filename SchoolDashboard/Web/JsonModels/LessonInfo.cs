using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web.JsonModels
{
    class LessonInfo
    {
        public bool IsLesson { get; set; }
        public string Description { get; set; }
        public int MinutesRemaining { get; set; }
    }
}
