using System;
using System.Collections.Generic;
using System.IO;

namespace ConwaysGameOfLife
{
	internal class UserConfig
	{
		public UserConfig(bool isRandom, bool isPreset, string presetPath, bool useHard, bool useBuffer, bool useWrap, float prosperity)
		{
			Random = isRandom;
			Preset = isPreset;
			this.presetPath = presetPath;
			Hard = useHard;
			Buffer = useBuffer;
			Wrap = useWrap;
			Prosperity = prosperity;
		}

		public bool Random { get; }

		private readonly string presetPath;

		public bool Preset { get; }

		public string PathName()
		{
			var saveLocation = @"C:\Users\cgrange\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\Data\";
			var fileType = ".txt";
			return $"{saveLocation}{presetPath}{fileType}";
		}

		public bool Hard { get; }

		public bool Buffer { get; }

		public bool Wrap { get; }

		public float Prosperity { get; }

		public static UserConfig GetUserInput()
		{
			ConsoleKeyInfo cki;
			ConsoleKey key;
			var currentSelection = 0;

			var options = new List<(string, string)>();

			var folders = Directory.GetDirectories(@"..\..\Data\");

			foreach (var folder in folders)
			{
				var files = Directory.GetFiles(folder);

				foreach (var file in files)
				{
					options.Add((Path.GetFileName(folder), Path.GetFileNameWithoutExtension(file)));
				}
			}

			do
			{
				Menus.StartMenu();
				cki = Console.ReadKey(true);
				Console.Clear();
			} while (cki.Key != ConsoleKey.Enter);

			var isRandom = true;
			var isPreset = false;
			do
			{
				Menus.PrintPatternSelectionMenu(isRandom, isPreset);
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
				Menus.PrintBoundarySelectionMenu(useHard, useBuffer, useWrap);

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
				do
				{
					Console.Clear();

					for (var i = 0; i < options.Count; ++i)
					{
						if (i == currentSelection)
						{
							Console.Write('\u25BA');
						}
						Console.WriteLine(options[i].Item2);
					}

					key = Console.ReadKey(true).Key;

					switch (key)
					{
						case ConsoleKey.UpArrow:
							{
								currentSelection = currentSelection - 1 < 0 ? options.Count - 1 : --currentSelection;
								break;
							}
						case ConsoleKey.DownArrow:
							{
								currentSelection = (currentSelection + 1) % options.Count;
								break;
							}
					}
					presetName = options[currentSelection].Item1 + "/" + options[currentSelection].Item2;
				} while (key != ConsoleKey.Enter);
			}

			var percentAlive = 0.50f;
			if (isRandom)
			{
				Menus.PrintRandomSelectionMenu();
				percentAlive = float.Parse("0." + Console.ReadLine());
			}

			var presetPath = isPreset
				? presetName
				: string.Empty;
			return new UserConfig(isRandom, isPreset, presetPath, useHard, useBuffer, useWrap, percentAlive);
		}
	}
}