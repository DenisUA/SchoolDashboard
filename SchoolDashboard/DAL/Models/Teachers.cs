using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    [TableName("Teachers")]
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int BirthdayDay { get; set; }
        public int BirthdayMounth { get; set; }
        public bool IsMale { get; set; }
    }
}
