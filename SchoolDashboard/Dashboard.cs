using Nancy;
using Nancy.Hosting.Self;
using SchoolDashboard.Controllers;
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

        public LessonsController Lessons { get; set; }

        public Dashboard()
        {
            InitWebHost();
            InitControllers();
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

        private void InitWebHost()
        {
            var hostConfig = new HostConfiguration();
            hostConfig.UrlReservations.CreateAutomatically = true;
            _webHost = new NancyHost(hostConfig, new Uri("http://localhost:8000"));
            StaticConfiguration.Caching.EnableRuntimeViewUpdates = true;
            StaticConfiguration.Caching.EnableRuntimeViewUpdates = true;
        }

        private void InitControllers()
        {
            Lessons = new LessonsController();
        }
    }
}
