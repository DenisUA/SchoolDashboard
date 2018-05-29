using SchoolDashboard.Common;
using SchoolDashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web.Models
{
    public class Award : DAL.Models.Awards
    {
        public string ImageName { get { return "/Images/awards/" + Helpers.AwardTypeToImageName(Type); } }

        public Award()
        {

        }

        public Award(DAL.Models.Awards model)
        {
            Id = model.Id;
            Owner = model.Owner;
            Description = model.Description;
            Type = model.Type;
        }

        public Dictionary<AwardsTypes, string> AllAwards
        {
            get
            {
                return Helpers.GetAllAwards();
            }
        }
    }
}
