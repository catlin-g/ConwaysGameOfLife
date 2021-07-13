using System;
using System.Collections.Generic;
using System.IO;

namespace ConwaysGameOfLife
{
	internal class Menu
	{
		private static readonly UserConfig userConfig;

		static Menu() => userConfig = new UserConfig();

		public static UserConfig StartMenu()
		{
			WelcomeScreen();

			var startMenuOptions = new List<string>() { "Start", "Options", "Quit" };

			var menuActive = true;

			while (menuActive)
			{
				var userSelection = MenuSelection(startMenuOptions);

				switch (startMenuOptions[userSelection])
				{
					case "Start":
						Console.Clear();
						break;

					case "Options":
						Console.Clear();
						GoLOptions();
						break;

					case "Quit":
						Environment.Exit(0);
						break;
				}

				menuActive = false;
			}

			return userConfig;
		}

		private const char Space = ' ';

		public static void WelcomeScreen()
		{
			Console.WriteLine();
			Console.WriteLine($"{Space}█▀▀ ▄▀█ █▀▄▀█ █▀▀   █▀█ █▀▀   █   █ █▀▀ █▀▀");
			Console.WriteLine($"{Space}█▄█ █▀█ █ ▀ █ ██▄   █▄█ █▀    █▄▄ █ █▀  ██▄");
			Console.WriteLine();
		}

		private const char Arrow = '\u25BA';

		public static int MenuSelection(List<string> options)
		{
			var currentSelection = 0;
			ConsoleKey key;

			do
			{
				Console.Clear();

				WelcomeScreen();

				for (var i = 0; i < options.Count; ++i)
				{
					if (i == currentSelection)
					{
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine($"{Space}{Arrow}{options[i]}");
						Console.ForegroundColor = ConsoleColor.Gray;
					}
					else
					{
						Console.WriteLine($"{Space}{Space}{options[i]}");
					}
				}

				key = Console.ReadKey(true).Key;

				switch (key)
				{
					case ConsoleKey.UpArrow:
						{
							currentSelection = currentSelection - 1 < 0
								? options.Count - 1
								: --currentSelection;
							break;
						}
					case ConsoleKey.DownArrow:
						{
							currentSelection = (currentSelection + 1) % options.Count;
							break;
						}
				}
			} while (key != ConsoleKey.Enter);

			return currentSelection;
		}

		public static void PrintRandomSelectionMenu()
		{
			RemoveConsoleFlicker();

			Console.WriteLine("Type the percentage of cells to be alive (0-100) and press 'Enter'.");
		}

		public static void RemoveConsoleFlicker()
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
		}

		public static void GoLOptions()
		{
			var golOptions = new List<string>() { "Seed", "Boundary", "Back" };
			var userSelection = MenuSelection(golOptions);

			switch (golOptions[userSelection])
			{
				case "Seed":
					Console.Clear();
					SeedOptions();
					break;

				case "Boundary":
					Console.Clear();
					BoundaryOptions();
					break;

				case "Back":
					Console.Clear();
					_ = StartMenu();
					break;
			}
		}

		public static void SeedOptions()
		{
			var seedOptions = new List<string>() { "Random", "Preset", "Back" };
			var userSelection = MenuSelection(seedOptions);

			switch (seedOptions[userSelection])
			{
				case "Random":
					Console.Clear();
					RandomSeedOptions();
					break;

				case "Preset":
					Console.Clear();
					userConfig.Preset = true;
					userConfig.Random = false;
					PresetOptions();
					break;

				case "Back":
					Console.Clear();
					GoLOptions();
					break;
			}
		}

		public static void BoundaryOptions()
		{
			var boundaryOptions = new List<string>() { "Hard", "Wrap", "Buffer", "Back" };
			var userSelection = MenuSelection(boundaryOptions);

			switch (boundaryOptions[userSelection])
			{
				case "Hard":
					Console.Clear();
					break;

				case "Wrap":
					Console.Clear();
					break;

				case "Buffer":
					Console.Clear();
					break;

				case "Back":
					Console.Clear();
					GoLOptions();
					break;
			}
		}

		public static void RandomSeedOptions()
		{
			Console.WriteLine("Type a value between 0-100 to determine the percentage of cells starting alive: ");
			var val = float.Parse(Console.ReadLine());

			while (!(0 <= val && val <= 100))
			{
				Console.WriteLine("Value was not within the correct range.");
				Console.WriteLine("Type a value between 0-100 to determine the percentage of cells starting alive: ");
				val = float.Parse(Console.ReadLine());
			}

			userConfig.Prosperity = val * 0.01f;
		}

		private const string DataLocation = @"..\..\Data\";

		public static void PresetOptions()
		{
			var presetOptions = new List<string>();

			var folders = Directory.GetDirectories(DataLocation);

			foreach (var folder in folders)
			{
				var files = Directory.GetFiles(folder);

				foreach (var file in files)
				{
					presetOptions.Add(Path.GetFileName(folder) + "/" + Path.GetFileNameWithoutExtension(file));
				}
			}

			presetOptions.Add("Back");

			var userSelection = MenuSelection(presetOptions);

			switch (presetOptions[userSelection])
			{
				case "Back":
					Console.Clear();
					SeedOptions();
					break;

				default:
					Console.Clear();
					userConfig.PresetPath = presetOptions[userSelection];
					break;
			}
		}
	}
}