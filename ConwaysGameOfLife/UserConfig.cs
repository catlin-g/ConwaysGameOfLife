using System;

namespace ConwaysGameOfLife
{
	class UserConfig
	{
		private readonly string presetPath;

		const int DefaultWidth = 20;

		public UserConfig(int width, int height, bool isRandom, string presetPath, bool useBuffer, bool useWrap, float prosperity)
		{
			Width = width;
			Height = height;
			UseRandom = isRandom;
			this.presetPath = presetPath;
			UseBuffer = useBuffer;
			UseWrap = useWrap;
			Prosperity = prosperity;
		}

		public int Width { get; }

		public int Height { get; }

		public bool UseRandom { get; }

		public bool UsePreset
			=> !string.IsNullOrEmpty(presetPath);

		public bool UseBuffer { get; }

		public bool UseWrap { get; }

		public float Prosperity { get; }

		public string GetPath()
		{
			const string saveLocation = @"C:\Users\cgrange\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\Data\";
			const string fileType = ".txt";
			return $"{saveLocation} + {presetPath} + {fileType}";
		}

		public static UserConfig GetUserInput()
		{
			var presetName = "StillLife/Block";
			var percentAlive = 0.50f;
			var width = 20;
			var height = 20;

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
					useBuffer = false;
					useWrap = false;
				}
				if (cki.Key == ConsoleKey.B)
				{
					useBuffer = true;
					useWrap = false;
					useHard = false;
				}
				if (cki.Key == ConsoleKey.W)
				{
					useWrap = true;
					useBuffer = false;
					useHard = false;
				}
			} while (cki.Key != ConsoleKey.Enter);

			Console.Clear();

			if (isPreset)
			{
				PrintMenus.PrintPresetSelectionMenu();
				presetName = Console.ReadLine();
				width = 20;
				height = 20;
			}

			if (isRandom)
			{
				PrintMenus.PrintRandomSelectionMenu();
				percentAlive = float.Parse("0." + Console.ReadLine());

				width = DefaultWidth;
				height = 20;
			}

			var presetPath = isPreset
				? presetName
				: string.Empty;

			return new UserConfig(width, height, isRandom, presetPath, useBuffer, useWrap, percentAlive);
		}
	}
}