using System;

namespace ConwaysGameOfLife
{
	class Program
	{
		public static UserConfig HandleUserInput()
		{
			ConsoleKeyInfo cki;

			Console.WriteLine("R => use random seed");
			Console.WriteLine("P => use a preset");
			Console.WriteLine("B => buffer");
			Console.WriteLine("W => wrap");
			Console.WriteLine("Enter => continue");

			var isRandom = false ;
			var isPreset = false;
			var useBuffer = false;
			var useWrap = false;
			var presetName = "";

			do
			{
				cki = Console.ReadKey();

				if (cki.Key == ConsoleKey.R)
				{
					isRandom = true;
				}
				if (cki.Key == ConsoleKey.P)
				{
					isPreset = true;
				}
				if (cki.Key == ConsoleKey.B)
				{
					useBuffer = true;
				}
				if (cki.Key == ConsoleKey.W)
				{
					useWrap = true;
				}
			} while (cki.Key != ConsoleKey.Enter);


			if (isPreset)
			{
				Console.WriteLine("Type the name of the preset to load and press 'Enter'.");

				presetName = Console.ReadLine();
			}

			var thing = new UserConfig(isRandom, presetName, useBuffer, useWrap);

			return thing;
		}

		static void Main(string[] args)
		{
			var userSettings = HandleUserInput();
			var gameOfLife = new GameOfLife(userSettings);

			gameOfLife.Initialise();
			gameOfLife.Run();
		}
	}
}