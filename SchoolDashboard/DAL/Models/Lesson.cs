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
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
        public string Description { get; set; }

        public DateTime StartTime
        {
            get
            {
                return DateTime.Parse(StartTimeString);
            }
            set
            {
                StartTimeString = value.ToString();
            }
        }

        public DateTime EndTime
        {
            get
            {
                return DateTime.Parse(EndTimeString);
            }
            set
            {
                EndTimeString = value.ToString();
            }
        }
    }
}
