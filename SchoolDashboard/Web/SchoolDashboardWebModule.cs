using Nancy;
using Nancy.ModelBinding;
using SchoolDashboard.Web.RequestsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web
{
    public class SchoolDashboardWebModule : WebModule
    {
        public dynamic GetLessonsInfo()
        {
            var model = this.Bind<LessonsInfoModel>();
            return Response.AsJson(Dashboard.GetInstance().Lessons.GetLessonInfo(model.SchoolLevel));
        }
    }
}
