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
            var lessons = Repository.GetLessons(schoolLevel).OrderBy(l => l.StartTime).ToArray();
            var currentTime = DateTime.Now.TimeOfDay;

            if (lessons[lessons.Length - 1].EndTime < currentTime || lessons[0].StartTime > currentTime)
                return LessonInfo.AsLessonsEnded();

            for (int i = 0; i < lessons.Length; i++)
            {
                if (lessons[i].StartTime <= currentTime && currentTime <= lessons[i].EndTime)
                    return LessonInfo.AsLesson(lessons[i].Description, (int)Math.Round((lessons[i].EndTime - currentTime).TotalMinutes, MidpointRounding.AwayFromZero));

                if (lessons[i].EndTime < currentTime && currentTime < lessons[i+1].StartTime)
                    return LessonInfo.AsBreak((int)Math.Round((lessons[i + 1].StartTime - currentTime).TotalMinutes, MidpointRounding.AwayFromZero));
            }

            return null;
        }
    }
}
