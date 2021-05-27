using System;
using System.Text;

namespace ConwaysGameOfLife
{
	class Program
	{
		private static void Main()
		{
			Console.Title = "Conway's Game of Life";
			Console.OutputEncoding = Encoding.Unicode;
			Console.CursorVisible = false;
			Console.WindowWidth = 90;
			Console.BufferWidth = 90;
			Console.WindowHeight = 40;
			Console.BufferHeight = 40;
			Console.BackgroundColor = ConsoleColor.Gray;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Black;

			var userConfig = UserConfig.GetUserInput();
			var gameOfLife = new GameOfLife(userConfig);
			gameOfLife.Initialise();
			gameOfLife.Run();
		}
	}
}