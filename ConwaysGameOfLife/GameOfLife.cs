using System;
using System.Threading;

namespace ConwaysGameOfLife
{
	internal class GameOfLife
	{
		private bool[,] cellsA;
		private bool[,] cellsB;
		private bool[,] cellsDraw;
		private bool[,] cellsUpdate;

		private readonly bool aliveValue = true;
		private int generation = 0;

		private readonly int size = 6;
		private readonly int numberOfRows;
		private readonly int numberOfCols;

		private readonly int factor = 2; // Affects the random seed.
		private readonly bool prosperous = false;

		/// <summary>
		/// Create a new Conway's Game of Life.
		///
		/// Size = 3 => [row, col] => {{T, F, F},
		//                            { F, T, F},
		//                            { F, T, T}}
		/// </summary>
		public GameOfLife()
		{
			numberOfRows = numberOfCols = size;
			cellsA = new bool[numberOfRows, numberOfCols];
			cellsB = new bool[numberOfRows, numberOfCols];
			cellsDraw = cellsA;
			cellsUpdate = cellsB;
		}

		/// <summary>
		/// Creates a random starting seed.
		///
		/// The 2D array is populated with either T or F.
		///
		/// The factor variable can be adjusted to increase or decrease the number
		/// of alive or dead cells. A random number can be between [0, 2 + factor].
		/// The prosperous switch is used to determine whether there will be more
		/// alive cells than dead cells to when the seed is created.
		/// </summary>
		private void GenerateRandomSeed()
		{
			var random = new Random(800);

			for (var i = 0; i < size; i++)
			{
				for (var j = 0; j < size; j++)
				{
					cellsDraw[i, j] = prosperous
						? Convert.ToBoolean(random.Next(0, 2 + factor))
						: !Convert.ToBoolean(random.Next(0, 2 + factor));
				}
			}
		}

		/// <summary>
		/// Draws the current generation/iteration to the console using
		/// a black square as the alive cell.
		/// </summary>
		private void DrawCurrentGeneration()
		{
			var blackSquare = "\u25A0"; // Alive cell

			for (var y = 0; y < size; y++)
			{
				for (var x = 0; x < size; x++)
				{
					if (x == 0)
					{
						Console.WriteLine();
					}
					var draw = this.cellsDraw[y, x] ? blackSquare + " " : "  ";
					Console.Write(draw);
				}
			}

			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Generation: " + generation);
		}

		private void GenerateNextGeneration()
		{
			for (var y = 0; y < cellsDraw.GetLength(0); y++)
			{
				for (var x = 0; x < cellsDraw.GetLength(1); x++)
				{
					var aliveNeighbours = GetAliveNeighbours(cellsDraw, y, x);
					var checkNeighbours = (aliveNeighbours == 2) || (aliveNeighbours == 3);
					var cellAlive = cellsDraw[y, x];

					cellsUpdate[y, x] = (cellAlive && checkNeighbours) || (!cellAlive && (aliveNeighbours == 3));
				}
			}

			cellsDraw = cellsA;
			cellsUpdate = cellsB;
			cellsA = cellsB;
			cellsB = cellsDraw;

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

		/// <summary>
		/// Initialize new Game Of Life instance.
		/// </summary>
		public void Initialise()
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);

			GenerateRandomSeed();
			DrawCurrentGeneration();

			Thread.Sleep(2000);
		}

		/// <summary>
		/// Run Conway's Game of Life.
		/// </summary>
		public void Run()
		{
			while (true)
			{
				Console.CursorVisible = false;
				Console.SetCursorPosition(0, 0);

				GenerateNextGeneration();
				DrawCurrentGeneration();

				Thread.Sleep(2000);
			}
		}
	}
}