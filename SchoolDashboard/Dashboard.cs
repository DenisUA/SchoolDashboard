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
        private static object _lockObj = new Object();
        private static Dashboard _instance;

        public LessonsController Lessons { get; set; }
        public TilesController Tiles { get; set; }

        private Dashboard()
        {
            InitWebHost();
            InitControllers();
        }

        public static Dashboard GetInstance()
        {
            lock (_lockObj)
            {
                if (_instance == null)
                    _instance = new Dashboard();
            }

            return _instance;
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
            Tiles = new TilesController();
        }
    }
}
