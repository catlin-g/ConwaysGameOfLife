using System;

namespace ConwaysGameOfLife
{
	class Cells
	{
		private readonly bool[,] cells;
		private int numOfCellsAlive;
		public char AliveCellSymbol = '\u25A0';
		public int Width => cells.GetLength(1);
		public int Height => cells.GetLength(0);

		public int buffer;
		public bool Wrap;

		public Cells(int width, int height)
		{
			/*if (width == 0)
			{
				throw new ArgumentException("width cannot be 0", nameof(width));
			}
			if (height == 0)
			{
				throw new ArgumentException("height cannot be 0", nameof(height));
			}*/

			cells = new bool[height + buffer, width + buffer];
		}

		public void SetValue(int x, int y, bool value)
		{
			/*if (x < 0 || x >= Width)
			{
				throw new ArgumentException("width cannot be less than 0 or greater than the grid width");
			}
			if (y < 0 || y >= Height)
			{
				throw new ArgumentException("height cannot be less than 0 or greater than the grid height");
			}*/

			cells[GetCoordinate(y, Height), GetCoordinate(x, Width)] = value;
		}

		public bool GetValue(int x, int y)
		{
			/*if (x < 0 || x >= Width)
			{
				throw new ArgumentException("width cannot be less than 0 or greater than the grid width");
			}
			if (y < 0 || y >= Height)
			{
				throw new ArgumentException("height cannot be less than 0 or greater than the grid height");
			}*/

			return cells[GetCoordinate(y, Height), GetCoordinate(x, Width)];
		}

		private int GetCoordinate(int dividend, int divisor)
		{
			return Wrap ? ModuloCore(dividend, divisor) : dividend;
		}

		private static int ModuloCore(int dividend, int divisor)
			=> ((dividend % divisor) + divisor) % divisor;

		public int GetPopulationCount()
			=> numOfCellsAlive;

		public void Update(Cells currentState)
		{
			numOfCellsAlive = 0;

			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Width; x++)
				{
					var aliveNeighbours = GetAliveNeighbours(x, y, currentState);
					var checkNeighbours = (aliveNeighbours == 2) || (aliveNeighbours == 3);
					var cellAlive = currentState.GetValue(x, y);

					var isAlive = (cellAlive && checkNeighbours) || (!cellAlive && (aliveNeighbours == 3));
					SetValue(x, y, isAlive);

					if (isAlive)
					{
						numOfCellsAlive++;
					}
				}
			}
		}
		private static int GetAliveNeighbours(int cellX, int cellY, Cells currentState)
		{
			var aliveNeighbours = 0;

			for (var y = cellY - 1; y < cellY + 2; y++)
			{

				var validY = (y >= 0) && (y <= (currentState.Height - 1));

				if (!validY && !currentState.Wrap)
				{
					continue;
				}

				for (var x = cellX - 1; x < cellX + 2; x++)
				{
					var validX = (x >= 0) && (x <= (currentState.Width - 1));

					var thisCell = (y == cellY) && (x == cellX);

					if ((validX || currentState.Wrap) && !thisCell && currentState.GetValue(x, y))
					{
						aliveNeighbours++;
					}
				}
			}
			return aliveNeighbours;
		}

		public void DrawConsole()
		{
			for (var y = buffer; y < Height - buffer; y++)
			{
				for (var x = buffer; x < Width - buffer; x++)
				{
					if (x == buffer)
					{
						Console.WriteLine();
					}
					var draw = GetValue(x, y) ? AliveCellSymbol + " " : " .";
					Console.Write(draw);
				}
			}

			Console.WriteLine();
			Console.WriteLine("******************");

			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Width; x++)
				{
					if (x == 0)
					{
						Console.WriteLine();
					}
					var draw = GetValue(x, y) ? AliveCellSymbol + " " : " .";
					Console.Write(draw);
				}
			}
		}
	}
}