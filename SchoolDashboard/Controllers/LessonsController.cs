using SchoolDashboard.DAL;
using SchoolDashboard.Web.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Controllers
{
    class LessonsController
    {
        public LessonInfo GetLessonInfo(int schoolLevel)
        {
            var lessons = Repository.GetLessons(schoolLevel).OrderBy(l => l.StartTime);

            return null;
        }
    }
}
