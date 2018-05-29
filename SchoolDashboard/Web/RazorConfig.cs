using Nancy.ViewEngines.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard.Web
{
    public class RazorConfig : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "SchoolDashboard";
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "SchoolDashboard.Web";
            yield return "SchoolDashboard.Web.Models";
        }

        public bool AutoIncludeModelNamespace
        {
            get { return true; }
        }
    }
}
