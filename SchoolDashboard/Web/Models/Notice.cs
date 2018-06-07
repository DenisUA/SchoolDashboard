using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web.Models
{
    public class Notice : DAL.Models.Notice
    {
        public Notice()
        {

        }

        public Notice(DAL.Models.Notice notice)
        {
            Id = notice.Id;
            Duration = notice.Duration;
            DateBinary = notice.DateBinary;
            Text = notice.Text;
            IsImportant = notice.IsImportant;
            Title = notice.Title;
        }

        public string Expired { get { return (Date + TimeSpan.FromDays(Duration)).ToShortDateString(); } }
    }
}
