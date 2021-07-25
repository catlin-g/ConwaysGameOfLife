using System;
using System.Text;

namespace ConwaysGameOfLife
{
	internal class Program
	{
		private const int ConsoleWindowWidth = 90;
		private const int ConsoleWindowHeight = 30;

		private static void Main()
		{
			Console.OutputEncoding = Encoding.Unicode;
			Console.CursorVisible = false;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.SetWindowSize(ConsoleWindowWidth, ConsoleWindowHeight);
			Console.SetBufferSize(ConsoleWindowWidth, ConsoleWindowHeight);
			Console.Title = "Conway's Game of Life";

			var driver = new Driver();
			driver.Start();
		}
	}
}