using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web.Models
{
    public class Awards
    {
        public Award[] ActiveAwards { get; set; }
        public Award[] UnactiveAwards { get; set; }
    }
}
