using System;
namespace SchoolDashboard
{
	public abstract class ConsoleCommand
	{
		public abstract bool IsMatch(string command);
		public abstract void Process(string command);
	}
}
