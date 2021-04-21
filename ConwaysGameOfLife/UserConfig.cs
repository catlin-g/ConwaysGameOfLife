using System;

namespace ConwaysGameOfLife
{
	class UserConfig
	{
		private readonly int width;
		private readonly int height;
		private readonly bool isRandom;
		private readonly string presetPath;
		private readonly bool useBuffer;
		private readonly bool useWrap;
		private readonly float prosperity;

		public UserConfig(int width, int height, bool isRandom, string presetPath, bool useBuffer, bool useWrap, float prosperity)
		{
			this.isRandom = isRandom;
			this.presetPath = presetPath;
			this.useBuffer = useBuffer;
			this.useWrap = useWrap;
			this.prosperity = prosperity;
			this.width = width;
			this.height = height;
		}

		public int GetWidth() => width;

		public int GetHeight() => height;

		public bool GetUseRandom() => isRandom;

		public string GetString()
		{
			var saveLocation = @"C:\Users\cgrange\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\Data\";
			var fileType = ".txt";
			return saveLocation + presetPath + fileType;
		}

		public bool GetUsePreset() => !string.IsNullOrEmpty(presetPath);

		public bool GetUseBuffer() => useBuffer;

		public bool GetUseWrap() => useWrap;

		public float GetProsperity() => prosperity;

		public static UserConfig UserInput()
		{
			ConsoleKeyInfo cki;
			var isRandom = true;
			var isPreset = false;
			var useHard = true;
			var useBuffer = false;
			var useWrap = false;
			var presetName = @"StillLife/Block";
			var percentAlive = 0.50f;
			var width = 20;
			var height = 20;

			do
			{
				PrintMenus.StartMenu();
				cki = Console.ReadKey(true);
				Console.Clear();
			} while (cki.Key != ConsoleKey.Enter);

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
					useBuffer = useHard = false;
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

				width = 20;
				height = 20;
			}

			return new UserConfig(width, height, isRandom, isPreset ? presetName : string.Empty, useBuffer, useWrap, percentAlive);
		}
	}
}