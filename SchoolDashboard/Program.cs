using SchoolDashboard.ConsoleCommands;
using SchoolDashboard.Controllers.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDashboard
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
            ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

            var dashboard = Dashboard.GetInstance();
            dashboard.Start();
            Console.WriteLine("Dashboard started. Press Enter(Return) to exit...");

            var commads = new ConsoleCommand[] { new LessonCirclesCommand(), new FixTileCommand() };

            while (true)
            {
                if (ProcessCommand(Console.ReadLine(), commads))
                    break;
            }
        }

        static bool ProcessCommand(string input, ConsoleCommand[] commads)
        {
            if (input.ToLower() == "exit")
                return true;

            foreach (var command in commads)
            {
                if (command.IsMatch(input))
                {
                    command.Process(input);
                    return false;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorrect command");
            Console.ResetColor();
            return false;
        }

        static bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

    }
}
