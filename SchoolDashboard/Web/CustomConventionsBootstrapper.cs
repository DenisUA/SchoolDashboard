using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web
{
    public class CustomConventionsBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            this.Conventions.ViewLocationConventions.Add((viewName, model, context) =>
            {
                return "Web/Views/" + viewName;
            });
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("CSS", "Web/Content/CSS"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("JS", "Web/Content/JS"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Fonts", "Web/Content/Fonts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Images", "Web/Content/Images"));
        }
    }
}
