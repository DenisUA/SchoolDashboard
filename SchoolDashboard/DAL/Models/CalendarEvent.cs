using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    class CalendarEvent
    {
        public int Id { get; set; }
        public long TimeBinary { get; set; }
        public bool HasTime { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }

        public DateTime DateTime
        {
            get { return DateTime.FromBinary(TimeBinary); }
            set { TimeBinary = value.ToBinary(); }
        }
    }
}
