using System;

namespace SchoolDashboard.ConsoleCommands
{
	public class LessonCirclesCommand : ConsoleCommand
	{
		public LessonCirclesCommand()
		{
		}

		public override bool IsMatch(string command)
		{
			//Console.WriteLine("Match working");
			return false;
		}

		public override void Process(string command)
		{
			Console.WriteLine("Process...");
		}
	}
}
