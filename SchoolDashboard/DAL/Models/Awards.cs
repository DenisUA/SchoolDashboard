using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.DAL.Models
{
    [TableName("Awards")]
    public class Awards
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public AwardsTypes Type { get; set; }
    }

    public enum AwardsTypes
    {
        Basketball = 0,
        Crown = 1,
        Diploma = 2,
        Football = 3,
        Medal = 4,
        Medal1 = 5,
        Medal2 = 6,
        Medal3 = 7,
        Shield = 8,
        Star = 9,
        Star1 = 10,
        Trophy = 11,
        Trophy1 = 12,
        Voleyboll = 13
    }
}
