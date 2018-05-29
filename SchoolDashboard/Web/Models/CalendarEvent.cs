using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web.Models
{
    public class CalendarEvent : SchoolDashboard.DAL.Models.CalendarEvent
    {
        public string DateTimeString { get; set; }

        public CalendarEvent()
        {
        }

        public CalendarEvent(SchoolDashboard.DAL.Models.CalendarEvent ev)
        {
            Id = ev.Id;
            TimeBinary = ev.TimeBinary;
            HasTime = ev.HasTime;
            Description = ev.Description;
            Place = ev.Place;
            DateTimeString = "";
        }
    }
}
