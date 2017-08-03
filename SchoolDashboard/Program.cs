using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard
{
    class Program
    {
        static void Main(string[] args)
        {
            var dashboard = Dashboard.GetInstance();
            dashboard.Start();
            Console.WriteLine("Dashboard started. Press Enter(Return) to exit...");

            Console.ReadLine();
            dashboard.Stop();
        }
    }
}
