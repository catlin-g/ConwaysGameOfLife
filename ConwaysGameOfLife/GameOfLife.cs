using System;
using System.IO;
using System.Threading;

namespace ConwaysGameOfLife
{
	internal class GameOfLife
	{
		private readonly Cells cellsA;
		private readonly Cells cellsB;

		private Cells cellsDraw;
		private Cells cellsUpdate;

		private readonly int size = 20;
		private readonly int width = 26;
		private readonly int height = 10;

		private readonly int factor = 2;
		private readonly bool prosperous = false;

		private bool isPreset = false;
		private string path = @"C:\Users\cgrange\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\Data\";

		private readonly Statistics statistics;

		public GameOfLife()
		{
			//width = height = size;
			cellsA = new Cells(width, height);
			cellsB = new Cells(width, height);
			cellsDraw = cellsA;
			cellsUpdate = cellsB;
			statistics = new Statistics();
		}

		private static void GenerateRandomSeed(Cells cells, bool prosperous, int factor)
		{
			var random = new Random();

			for (var y = 0; y < cells.Height; y++)
			{
				for (var x = 0; x < cells.Width; x++)
				{
					var value = prosperous
						? Convert.ToBoolean(random.Next(0, 2 + factor))
						: !Convert.ToBoolean(random.Next(0, 2 + factor));

					cells.SetValue(x, y, value);
				}
			}
		}

		private void DrawCurrentGeneration()
		{
			cellsDraw.DrawConsole();
		}

		private void DrawGUI()
		{
			statistics.Print();
		}

		private void GenerateNextGeneration()
		{
			cellsUpdate.Update(cellsDraw);

			var temp = cellsDraw;
			cellsDraw = cellsUpdate;
			cellsUpdate = temp;

			statistics.Change = cellsDraw.GetPopulationCount() - cellsUpdate.GetPopulationCount();
			statistics.PopulationSize = cellsDraw.GetPopulationCount();
			statistics.Generation++;
		}

		private void RemoveConsoleFlicker()
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
		}

		private void LoadState()
		{
			var totalLines = File.ReadAllLines(path);
			var cellsPreset = new Cells(totalLines.Length, totalLines[0].Length);
			var y = 0;

			foreach (var line in totalLines)
			{
				var x = 0;
				foreach (var character in line)
				{
					cellsPreset.SetValue(x, y, character == '1');
					x++;
				}
				y++;
			}

			var val = (totalLines[0].Length == width) && (totalLines.Length == height);

			cellsDraw = val
				? cellsPreset
				: Translate(cellsDraw, cellsPreset);
		}

		private Cells Translate(Cells canvas, Cells toPaste)
		{
			for (var y = 0; y < toPaste.Height; y++)
			{
				for (var x = 0; x < toPaste.Width; x++)
				{
					canvas.SetValue(x, y, toPaste.GetValue(x, y));
				}
			}
			return canvas;
		}

		public void HandleUserInput()
		{
			ConsoleKeyInfo cki;

			Console.WriteLine("Press 'R' to generate a random seed or 'P' to select a preset.");
			Console.WriteLine("Press 'Enter' to continue.");

			do
			{
				cki = Console.ReadKey();
				if (cki.Key == ConsoleKey.R)
				{
					isPreset = false;
				}
				if (cki.Key == ConsoleKey.P)
				{
					isPreset = true;
				}
			} while (cki.Key != ConsoleKey.Enter);

			if (isPreset)
			{
				Console.WriteLine("Type the name of the preset to load and press 'Enter'.");

				var userInput = Console.ReadLine();
				path += userInput + ".txt";
			}
		}

		public void Initialise()
		{
			Console.Clear();

			RemoveConsoleFlicker();

			if (isPreset)
			{
				LoadState();
			}
			else
			{
				GenerateRandomSeed(cellsDraw, prosperous, factor);
			}

			DrawCurrentGeneration();
			DrawGUI();

			Thread.Sleep(200);
		}

		public void Run()
		{
			var cellsAlive = true;

			while (cellsAlive)
			{
				RemoveConsoleFlicker();

				GenerateNextGeneration();
				DrawCurrentGeneration();
				DrawGUI();

				Thread.Sleep(200);
			}
		}
	}
}