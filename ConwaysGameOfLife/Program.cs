using System;

namespace ConwaysGameOfLife
{
	class Program
	{
		private static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;

			var userConfig = UserConfig.UserInput();
			var gameOfLife = new GameOfLife(userConfig);
			gameOfLife.Initialise();
			gameOfLife.Run();
		}
	}
}