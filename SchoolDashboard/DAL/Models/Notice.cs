using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    [TableName("Notices")]
    public class Notice
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long DateBinary { get; set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public bool IsImportant { get; set; }

        public DateTime Date
        {
            get { return DateTime.FromBinary(DateBinary); }
            set { DateBinary = value.ToBinary(); }
        }

        public string DateString
        {
            get { return Date.ToShortDateString(); }
        }
    }
}
