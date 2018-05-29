using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    [TableName("SchoolLevels")]
    class SchoolLevel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
