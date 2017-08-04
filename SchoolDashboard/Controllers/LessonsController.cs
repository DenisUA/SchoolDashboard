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
                {
                    var minutes = (int)Math.Round((lessons[i].EndTime - currentTime).TotalMinutes, MidpointRounding.AwayFromZero);
                    var perc = 100 - (minutes * 100 / (lessons[i].EndTime.TotalMinutes - lessons[i].StartTime.TotalMinutes));
                    return LessonInfo.AsLesson(lessons[i].Description, minutes, (int)perc);
                }

                if (lessons[i].EndTime < currentTime && currentTime < lessons[i+1].StartTime)
                {
                    var minutes = (int)Math.Round((lessons[i + 1].StartTime - currentTime).TotalMinutes, MidpointRounding.AwayFromZero);
                    var perc = 100 - (minutes * 100 / (lessons[i + 1].StartTime.TotalMinutes - lessons[i].EndTime.TotalMinutes));
                    return LessonInfo.AsBreak(minutes, (int)perc);
                }
            }

            return null;
        }
    }
}
