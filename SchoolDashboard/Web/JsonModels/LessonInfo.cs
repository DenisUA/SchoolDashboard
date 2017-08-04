using SchoolDashboard.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web.JsonModels
{
    class LessonInfo
    {
        public bool LessonsEnded { get; set; }
        public bool IsLesson { get; set; }
        public string Description { get; set; }
        public int MinutesRemaining { get; set; }
        public int Percantage { get; set; }

        public static LessonInfo AsLessonsEnded()
        {
            return new LessonInfo()
            {
                LessonsEnded = true,
                Description = StringConstants.LessonsEnded
            };
        }

        public static LessonInfo AsLesson(string lessonName, int minutesRemaining, int percentage)
        {
            return new LessonInfo()
            {
                LessonsEnded = false,
                IsLesson = true,
                Description = lessonName,
                MinutesRemaining = minutesRemaining,
                Percantage = percentage
            };
        }

        public static LessonInfo AsBreak(int minutesRemaining, int percentage)
        {
            return new LessonInfo()
            {
                LessonsEnded = false,
                IsLesson = false,
                Description = StringConstants.Break,
                MinutesRemaining = minutesRemaining,
                Percantage = percentage
            };
        }
    }
}
