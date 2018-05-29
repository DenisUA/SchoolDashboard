using Nancy;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web
{
    public class WebModule : NancyModule
    {
        internal virtual string PathPrefix { get; }

        public WebModule()
        {
            var prefix = PathPrefix;
            if (prefix == null || prefix == "")
            {
                Get["/"] = p => Process(null);
                Post["/"] = p => Process(null);
                Get["/{page}"] = p => Process(p);
                Post["/{page}"] = p => Process(p);
            }
            else
            {
                Get["/" + prefix + "/"] = p => Process(null);
                Post["/" + prefix + "/"] = p => Process(null);
                Get["/" + prefix + "/{page}"] = p => Process(p);
                Post["/" + prefix + "/{page}"] = p => Process(p);
            }
        }

        private dynamic Process(dynamic parameters)
        {
            if (parameters == null || parameters.page == null)
                return Redirect(Index);

            return InvokeMethodFromClass(parameters.page, Request.Query.Keys.Count > 0 ? Request.Query : Request.Form);
        }

        public virtual dynamic Index()
        {

            return GetView();
        }

        public dynamic InvokeMethodFromClass(string methodName, dynamic parameters)
        {
            var type = this.GetType();
            var method = type.GetMethod(methodName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (method == null)
                throw new ArgumentException(string.Format("No method {0} in class {1}", methodName, type.Name));

            return method.Invoke(this, method.GetParameters().Length > 0 ? new dynamic[] { parameters } : new dynamic[0]);
        }

        public Negotiator GetView([CallerMemberName] string callerName = "")
        {
            ViewBag.ViewName = callerName;
            return View[callerName];
        }

        public Negotiator GetView(object model, [CallerMemberName] string callerName = "")
        {
            ViewBag.ViewName = callerName;
            return View[callerName, model];
        }

        public dynamic Redirect(Func<dynamic> function)
        {
            return Response.AsRedirect(function.GetMethodInfo().Name);
        }
    }
}
