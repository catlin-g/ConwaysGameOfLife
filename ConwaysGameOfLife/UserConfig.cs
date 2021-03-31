using System;

namespace ConwaysGameOfLife
{
	class UserConfig
	{
		private static readonly char arrow = '\u25BA';

		private readonly bool isRandom;
		private readonly string path;
		private readonly bool useBuffer;
		private readonly bool useWrap;
		private readonly float prosperity;

		public UserConfig(bool isRandom, string path, bool useBuffer, bool useWrap, float prosperity)
		{
			this.isRandom = isRandom;
			this.path = path;
			this.useBuffer = useBuffer;
			this.useWrap = useWrap;
			this.prosperity = prosperity;
		}

		public bool GetUseRandom() => isRandom;

		public bool GetUseBuffer() => useBuffer;

		public string GetString()
		{
			var saveLocation = @"C:\Users\cgrange\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\Data\";
			var fileType = ".txt";
			return saveLocation + path + fileType;
		}

		public bool GetUsePreset() => !string.IsNullOrEmpty(path);

		public bool GetUseWrap() => useWrap;

		public float GetProsperity() => prosperity;

		public static UserConfig UserInput()
		{
			// Initial pattern / seed
			var isRandom = true;
			var isPreset = false;

			// Boundary type
			var useHard = true;
			var useBuffer = false;
			var useWrap = false;

			// If Preset
			var presetName = @"StillLife/Block";

			// If Random
			var percentAlive = 0.50f;

			ConsoleKeyInfo cki;

			do
			{
				PrintPatternSelectionMenu(isRandom, isPreset);
				cki = Console.ReadKey(true);

				if (cki.Key == ConsoleKey.R)
				{
					isRandom = true;
					isPreset = false;
				}
				if (cki.Key == ConsoleKey.P)
				{
					isRandom = false;
					isPreset = true;
				}
			} while (cki.Key != ConsoleKey.Enter);

			Console.Clear();

			do
			{
				PrintBoundarySelectionMenu(useHard, useBuffer, useWrap);

				cki = Console.ReadKey(true);

				if (cki.Key == ConsoleKey.H)
				{
					useHard = true;
					useBuffer = useWrap = false;
				}
				if (cki.Key == ConsoleKey.B)
				{
					useBuffer = true;
					useWrap = useHard = false;
				}
				if (cki.Key == ConsoleKey.W)
				{
					useWrap = true;
					useBuffer = useHard = false;
				}
			} while (cki.Key != ConsoleKey.Enter);

			Console.Clear();

			if (isPreset)
			{
				PrintPresetSelectionMenu();
				presetName = Console.ReadLine();
			}

			if (isRandom)
			{
				PrintRandomSelectionMenu();
				percentAlive = float.Parse("0." + Console.ReadLine());
			}

			return new UserConfig(isRandom, isPreset ? presetName : string.Empty, useBuffer, useWrap, percentAlive);
		}

		public static void PrintPatternSelectionMenu(bool isRandom, bool isPreset)
		{
			RemoveConsoleFlicker();

			Console.WriteLine("Select an initial pattern (seed):");
			Console.WriteLine(" " + (isRandom ? arrow : ' ') + " R => generate a random field");
			Console.WriteLine(" " + (isPreset ? arrow : ' ') + " P => use a preset");
			Console.WriteLine("Enter => continue");
		}

		public static void PrintBoundarySelectionMenu(bool useHard, bool useBuffer, bool useWrap)
		{
			RemoveConsoleFlicker();

			Console.WriteLine("Choose a boundary type:");
			Console.WriteLine(" " + (useHard ? arrow : ' ') + " H => hard boundary (cells die outside the active zone)");
			Console.WriteLine(" " + (useBuffer ? arrow : ' ') + " B => buffer (creates a hidden active zone)");
			Console.WriteLine(" " + (useWrap ? arrow : ' ') + " W => wrap (active zones move across and reappear on opposite edges)");
			Console.WriteLine("Enter => continue");
		}

		public static void PrintPresetSelectionMenu()
		{
			Console.WriteLine("Type the name of the preset to load and press 'Enter'.");
			Console.Write(arrow);
		}

		public static void PrintRandomSelectionMenu() => Console.WriteLine("Type the percentage of cells to be alive (0-100) and press 'Enter'.");

		private static void RemoveConsoleFlicker()
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
		}
	}
}