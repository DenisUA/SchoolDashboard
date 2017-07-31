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
        public WebModule()
        {
            Get["/"] = p => Process(null);
            Post["/"] = p => Process(null);
            Get["/{page}"] = p => Process(p);
            Post["/{page}"] = p => Process(p);
        }

        private dynamic Process(dynamic parameters)
        {
            if (parameters == null || parameters.page == null)
                return Redirect(Index);

            return InvokeMethodFromClass(parameters.page, Request.Query);
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

            return method.Invoke(this, new object[0]);
        }

        public Negotiator GetView([CallerMemberName] string callerName = "")
        {
            return View[callerName];
        }

        public Negotiator GetView(object model, [CallerMemberName] string callerName = "")
        {
            return View[callerName, model];
        }

        public dynamic Redirect(Func<object, dynamic> function)
        {
            return Redirect(function.GetMethodInfo());
        }

        public dynamic Redirect(Func<dynamic> function)
        {
            return Redirect(function.GetMethodInfo());
        }

        private dynamic Redirect(MethodInfo method)
        {
            return Response.AsRedirect(method.Name);
        }
    }
}
