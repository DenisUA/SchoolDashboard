using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    [TableName("Students")]
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int BirthdayDay { get; set; }
        public int BirthdayMounth { get; set; }
        public bool IsMale { get; set; }
    }
}
