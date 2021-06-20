using System;
using System.Text;

namespace ConwaysGameOfLife
{
	class Menus
	{
		public static void Initialise()
		{
			Console.Title = "Conway's Game of Life";
			Console.OutputEncoding = Encoding.Unicode;
			Console.CursorVisible = false;
			Console.WindowWidth = 90;
			Console.BufferWidth = 90;
			Console.WindowHeight = 30;
			Console.BufferHeight = 30;
			Console.BackgroundColor = ConsoleColor.Gray;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Black;
		}


		private static readonly char arrow = '\u25BA';

		public static void StartMenu()
		{
			RemoveConsoleFlicker();

			Console.WriteLine(@"   ____                         ___   __   _     _  __       ");
			Console.WriteLine(@"  / ___| __ _ _ __ ___   ___   / _ \ / _| | |   (_)/ _| ___  ");
			Console.WriteLine(@" | |  _ / _` | '_ ` _ \ / _ \ | | | | |_  | |   | | |_ / _ \ ");
			Console.WriteLine(@" | |_| | (_| | | | | | |  __/ | |_| |  _| | |___| |  _|  __/ ");
			Console.WriteLine(@"  \____|\__,_|_| |_| |_|\___|  \___/|_|   |_____|_|_|  \___| ");
			Console.WriteLine(@"                                                             ");

			Console.WriteLine("Follow the prompts to create a seed and then watch its evolution.");
			Console.WriteLine("Press Enter to start!");
		}

		public static void PrintPatternSelectionMenu(bool isRandom, bool isPreset)
		{
			RemoveConsoleFlicker();

			Console.WriteLine("Select an initial pattern (seed):");
			Console.WriteLine("   " + (isRandom ? arrow : ' ') + " [R] => generate a random field");
			Console.WriteLine("   " + (isPreset ? arrow : ' ') + " [P] => use a preset");
			Console.WriteLine();
			Console.WriteLine("Please make a selection and press Enter to continue.");
		}

		public static void PrintBoundarySelectionMenu(bool useHard, bool useBuffer, bool useWrap)
		{
			RemoveConsoleFlicker();

			Console.WriteLine("Choose a boundary type:");
			Console.WriteLine(" " + (useHard ? arrow : ' ') + " [H] => hard boundary (cells die outside the boundary)");
			Console.WriteLine(" " + (useBuffer ? arrow : ' ') + " [B] => buffer (creates a hidden active zone outside the boundary)");
			Console.WriteLine(" " + (useWrap ? arrow : ' ') + " [W] => wrap (active zones move across boundaries and reappear on opposite edges)");
			Console.WriteLine();
			Console.WriteLine("Please make a selection and press Enter to continue.");
		}

		public static void PrintRandomSelectionMenu()
		{
			RemoveConsoleFlicker();

			Console.WriteLine("Type the percentage of cells to be alive (0-100) and press 'Enter'.");
		}

		public static void PrintPresetSelectionMenu()
		{
			RemoveConsoleFlicker();

			Console.WriteLine("Choose a preset from the list below:");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
			Console.WriteLine(" " + arrow + " [1] => Oscillator/Beacon");
		}

		public static void RemoveConsoleFlicker()
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
		}
	}
}