using Nancy;
using Nancy.ModelBinding;
using SchoolDashboard.Web.RequestsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.DAL;

namespace SchoolDashboard.Web
{
    public class DashboardWebModule : WebModule
    {
        public dynamic GetLessonsInfo()
        {
            var model = this.Bind<LessonsInfoModel>();
            return Response.AsJson(Dashboard.GetInstance().Lessons.GetLessonInfo(model.SchoolLevel));
        }

        public dynamic GetSchoolLevels()
        {
            var levels = Repository.GetSchoolLevels();
            return Response.AsJson(levels);
        }

        public dynamic GetTileShowInfo()
        {
            var res = Dashboard.GetInstance().Tiles.GetShowTilesInfo();
            return Response.AsJson(res);
        }

        public dynamic IsTileFixed()
        {
            var res = Dashboard.GetInstance().Tiles.FixedTile != null;
            return Response.AsText(res.ToString());
        }

        internal override string PathPrefix
        {
            get
            {
                return "Dashboard";
            }
        }
    }
}
