using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    class Lesson
    {
        public int Id { get; set; }
        public double StartTimeSeconds { get; set; }
        public double EndTimeSeconds { get; set; }
        public string Description { get; set; }

        public TimeSpan StartTime
        {
            get
            {
                return TimeSpan.FromSeconds(StartTimeSeconds);
            }
            set
            {
                StartTimeSeconds = value.TotalSeconds;
            }
        }

        public TimeSpan EndTime
        {
            get
            {
                return TimeSpan.FromSeconds(EndTimeSeconds);
            }
            set
            {
                EndTimeSeconds = value.TotalSeconds;
            }
        }
    }
}
