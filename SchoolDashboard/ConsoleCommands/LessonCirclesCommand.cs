using System;
namespace SchoolDashboard
{
	public class LessonCirclesCommand : ConsoleCommand
	{
		public LessonCirclesCommand()
		{
		}

		public override bool IsMatch(string command)
		{
			Console.WriteLine("Match working");
			return true;
		}

		public override void Process(string command)
		{
			Console.WriteLine("Process...");
		}
	}
}
