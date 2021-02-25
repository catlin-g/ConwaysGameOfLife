using System;
using System.IO;
using System.Threading;

namespace ConwaysGameOfLife
{
	internal class GameOfLife
	{
		private readonly bool[,] cellsA;
		private readonly bool[,] cellsB;
		private bool[,] cellsDraw;
		private bool[,] cellsUpdate;

		private int generation = 0;
		private int populationSize;

		private readonly int size = 20;
		private readonly int numberOfRows;
		private readonly int numberOfCols;

		private readonly int factor = 2;
		private readonly bool prosperous = false;

		public GameOfLife()
		{
			numberOfRows = numberOfCols = size;
			cellsA = new bool[numberOfRows, numberOfCols];
			cellsB = new bool[numberOfRows, numberOfCols];
			cellsDraw = cellsA;
			cellsUpdate = cellsB;
		}

		private void GenerateRandomSeed()
		{
			var random = new Random();

			for (var y = 0; y < numberOfRows; y++)
			{
				for (var x = 0; x < numberOfCols; x++)
				{
					cellsDraw[y, x] = prosperous
						? Convert.ToBoolean(random.Next(0, 2 + factor))
						: !Convert.ToBoolean(random.Next(0, 2 + factor));
				}
			}
		}

		private void DrawCurrentGeneration()
		{
			var blackSquare = "\u25A0"; // Alive cell

			for (var y = 0; y < numberOfRows; y++)
			{
				for (var x = 0; x < numberOfCols; x++)
				{
					if (x == 0)
					{
						Console.WriteLine();
					}
					var draw = this.cellsDraw[y, x] ? blackSquare + " " : "  ";
					Console.Write(draw);
				}
			}
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
			populationSize = 0;

			for (var y = 0; y < numberOfRows; y++)
			{
				for (var x = 0; x < numberOfCols; x++)
				{
					var aliveNeighbours = GetAliveNeighbours(cellsDraw, y, x);
					var checkNeighbours = (aliveNeighbours == 2) || (aliveNeighbours == 3);
					var cellAlive = cellsDraw[y, x];

					cellsUpdate[y, x] = (cellAlive && checkNeighbours) || (!cellAlive && (aliveNeighbours == 3));
				}
			}

			var temp = cellsDraw;
			cellsDraw = cellsUpdate;
			cellsUpdate = temp;

			generation++;
		}

		private int GetAliveNeighbours(bool[,] cells, int cellY, int cellX)
		{
			var aliveNeighbours = 0;

			for (var y = cellY - 1; y < cellY + 2; y++)
			{
				var validRow = (y >= 0) && (y <= (numberOfRows - 1));

				if (!validRow)
				{
					continue;
				}

				for (var x = cellX - 1; x < cellX + 2; x++)
				{
					var validCol = (x >= 0) && (x <= (numberOfCols - 1));
					var thisCell = (y == cellY) && (x == cellX);

					if (validCol && !thisCell && cells[y, x])
					{
						aliveNeighbours++;
					}
				}
			}

			return aliveNeighbours;
		}

		private void RemoveConsoleFlicker()
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
		}


		// Currently, the file has to contain a minimum of 1 cells, and cannot be greater than what the board is initialised to.
		private void LoadState()
		{
			// Hard Coded Input
			var path = @"C:\Users\cgrange\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\Data\Spaceship\LightWeightSpaceShip.txt";
			var linearTranslate = 1;

			//
			var rows = File.ReadAllLines(path);
			var cellsPreset = new bool[rows.Length, rows[0].Length];
			var x = 0;
			var y = 0;

			foreach (var row in rows)
			{
				x = 0;
				foreach (var col in row)
				{
					cellsPreset[y, x] = col == '1';
					x++;
				}
				y++;
			}

			cellsDraw = (rows.Length == numberOfRows) && (rows[0].Length == numberOfCols) ? cellsPreset : Translate(cellsPreset);
		}

		public bool[,] Translate(bool[,] cells)
		{
			for (var y = 0; y < cells.GetLength(0); y++)
			{
				for (var x = 0; x < cells.GetLength(1); x++)
				{
					cellsDraw[y, x] = cells[y, x];
				}
			}
			return cellsDraw;
		}

		public void Initialise()
		{
			RemoveConsoleFlicker();

			//GenerateRandomSeed();
			LoadState();
			DrawCurrentGeneration();
			//DrawGUI();

			Thread.Sleep(2000);
		}

		public void Run()
		{
			var cellsAlive = true;


			while (cellsAlive)
			{
				RemoveConsoleFlicker();

				GenerateNextGeneration();
				DrawCurrentGeneration();
				//DrawGUI();

				Thread.Sleep(2000);
			}
		}
	}
}