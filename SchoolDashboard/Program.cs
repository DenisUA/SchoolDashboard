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

			var commads = new ConsoleCommand[] { new LessonCirclesCommand() };

			while (true) {
				ProcessCommand(Console.ReadLine(), commads);
			}
        }

		static void ProcessCommand(string input, ConsoleCommand[] commads)
		{
			foreach (var command in commads)
			{
				if (command.IsMatch(input))
				{
					command.Process(input);
					return;
				}
			}
			Console.WriteLine("Incorrect command");
		}
    }
}
