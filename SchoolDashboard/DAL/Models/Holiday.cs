using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    [TableName("Holidays")]
    public class Holiday
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public DateTime Date
        {
            get
            {
                return new DateTime(1, Month, Day).Date;
            }
        }

    }
}
