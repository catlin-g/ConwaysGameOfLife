using System;

namespace ConwaysGameOfLife
{
	internal class UserConfig
	{
		public UserConfig(int width, int height, bool isRandom, string presetPath, bool useBuffer, bool useWrap, float prosperity)
		{
			Width = width;
			Height = height;
			Random = isRandom;
			this.presetPath = presetPath;
			Buffer = useBuffer;
			Wrap = useWrap;
			Prosperity = prosperity;
		}

		public int Width { get; }

		public int Height { get; }

		public bool Random { get; }

		private readonly string presetPath;

		public string Path()
		{
			var saveLocation = @"C:\Users\cgrange\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\Data\";
			var fileType = ".txt";
			return $"{saveLocation}{presetPath}{fileType}";
		}

		public bool Preset
			=> !string.IsNullOrEmpty(presetPath);

		public bool Buffer { get; }

		public bool Wrap { get; }

		public float Prosperity { get; }

		public static UserConfig GetUserInput()
		{
			ConsoleKeyInfo cki;
			do
			{
				PrintMenus.StartMenu();
				cki = Console.ReadKey(true);
				Console.Clear();
			} while (cki.Key != ConsoleKey.Enter);

			var isRandom = true;
			var isPreset = false;
			do
			{
				PrintMenus.PrintPatternSelectionMenu(isRandom, isPreset);
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

			var useHard = true;
			var useBuffer = false;
			var useWrap = false;
			do
			{
				PrintMenus.PrintBoundarySelectionMenu(useHard, useBuffer, useWrap);

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
					useBuffer = false;
					useHard = false;
				}
			} while (cki.Key != ConsoleKey.Enter);

			Console.Clear();

			var presetName = @"StillLife/Block";
			if (isPreset)
			{
				PrintMenus.PrintPresetSelectionMenu();
				presetName = Console.ReadLine();
			}

			var percentAlive = 0.50f;
			if (isRandom)
			{
				PrintMenus.PrintRandomSelectionMenu();
				percentAlive = float.Parse("0." + Console.ReadLine());
			}

			var width = 20;
			var height = 20;
			var presetPath = isPreset
				? presetName
				: string.Empty;
			return new UserConfig(width, height, isRandom, presetPath, useBuffer, useWrap, percentAlive);
		}
	}
}