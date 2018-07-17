using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web.Models
{
    public class ExecuteSql
    {
        public string Request { get; set; }
        public string[][] Data { get; set; }
        public string[] Headers { get; set; }
    }
}
