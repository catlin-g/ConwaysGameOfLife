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

		private int generation = 0;
		private readonly int populationSize = 0;

		private readonly int size = 20;
		private readonly int numberOfRows;
		private readonly int numberOfCols;

		private readonly int factor = 2;
		private readonly bool prosperous = false;

		private bool isPreset = false;
		private string path = @"C:\Users\cgrange\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\Data\";

		public GameOfLife()
		{
			numberOfRows = numberOfCols = size;
			cellsA = new Cells(numberOfRows, numberOfCols);
			cellsB = new Cells(numberOfRows, numberOfCols);
			cellsDraw = cellsA;
			cellsUpdate = cellsB;
		}

		private static void GenerateRandomSeed(Cells cells, bool prosperous, int factor)
		{
			var random = new Random();

			for (var y = 0; y < cells.Rows; y++)
			{
				for (var x = 0; x < cells.Cols; x++)
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
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Generation: " + generation);
			Console.WriteLine("Population: " + populationSize);
		}

		private void GenerateNextGeneration()
		{
			cellsUpdate.Update(cellsDraw);

			var temp = cellsDraw;
			cellsDraw = cellsUpdate;
			cellsUpdate = temp;

			generation++;
		}

		private void RemoveConsoleFlicker()
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
		}

		// Currently, the file has to contain a minimum of 1 cells, and cannot be greater than what the board is initialised to.
		private void LoadState()
		{
			var rows = File.ReadAllLines(path);
			var cellsPreset = new Cells(rows.Length, rows[0].Length);
			var y = 0;

			foreach (var row in rows)
			{
				var x = 0;
				foreach (var col in row)
				{
					cellsPreset.SetValue(x, y, col == '1');
					x++;
				}
				y++;
			}

			var val = (rows.Length == numberOfRows) && (rows[0].Length == numberOfCols);

			cellsDraw = val
				? cellsPreset
				: Translate(cellsDraw, cellsPreset);
		}

		private Cells Translate(Cells canvas, Cells toPaste) //bool[,] cells)
		{
			for (var y = 0; y < toPaste.Rows; y++)
			{
				for (var x = 0; x < toPaste.Cols; x++)
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