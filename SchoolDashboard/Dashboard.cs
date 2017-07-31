using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard
{
    class Dashboard
    {
        private NancyHost _webHost;

        public Dashboard()
        {
            var hostConfig = new HostConfiguration();
            hostConfig.UrlReservations.CreateAutomatically = true;
            _webHost = new NancyHost(hostConfig, new Uri("http://localhost:8080"));
        }

        public void Start()
        {
            _webHost.Start();
        }

        public void Stop()
        {
            _webHost.Stop();
            _webHost.Dispose();
        }
    }
}
