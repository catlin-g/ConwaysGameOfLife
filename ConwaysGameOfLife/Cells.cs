using System;

namespace ConwaysGameOfLife
{
	interface IConsoleDrawable
	{
		void DrawConsole();
	}

	//interface IGuiDrawable
	//{
	//	void Draw();
	//}

	class Cells : IConsoleDrawable
	{
		private readonly bool[,] cells;
		private int numOfCellsAlive;
		public char AliveCellSymbol = '\u25A0';
		public int Width => cells.GetLength(1);
		public int Height => cells.GetLength(0);

		public Cells(int width, int height)
		{
			cells = new bool[height, width];
		}

		public void SetValue(int x, int y, bool value)
		{
			cells[y, x] = value;
		}

		public bool GetValue(int x, int y)
		{
			return cells[y, x];
		}

		public int GetPopulationCount()
		{
			return numOfCellsAlive;
		}

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

				if (!validY)
				{
					continue;
				}

				for (var x = cellX - 1; x < cellX + 2; x++)
				{
					var validX = (x >= 0) && (x <= (currentState.Width - 1));
					var thisCell = (y == cellY) && (x == cellX);

					if (validX && !thisCell && currentState.GetValue(x, y))
					{
						aliveNeighbours++;
					}
				}
			}
			return aliveNeighbours;
		}

		private static int GetAliveNeighboursWrap(int cellX, int cellY, Cells currentState)
		{
			var aliveNeighbours = 0;

			for (var y = cellY - 1; y < cellY + 2; y++)
			{
				for (var x = cellX - 1; x < cellX + 2; x++)
				{
					var thisCell = (y == cellY) && (x == cellX);

					if (!thisCell && (currentState.GetValue(MathMod(x, currentState.Width), MathMod(y, currentState.Height))))
					{
						aliveNeighbours++;
					}
				}
			}
			return aliveNeighbours;
		}

		private static int GetAliveNeighboursBuffer(int cellX, int cellY, Cells currentState)
		{
			var aliveNeighbours = 0;
			var buffer = 5;

			for (var y = cellY - 1; y < cellY + 2; y++)
			{
				for (var x = cellX - 1; x < cellX + 2; x++)
				{
					var thisCell = (y == cellY) && (x == cellX);

					if (!thisCell)
					{
						aliveNeighbours++;
					}
				}
			}
			return aliveNeighbours;
		}

		private static int MathMod(int a, int b)
		{
			return (Math.Abs(a * b) + a) % b;
		}

		public void DrawConsole()
		{
			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Width; x++)
				{
					if (x == 0)
					{
						Console.WriteLine();
					}
					var draw = GetValue(x, y) ? AliveCellSymbol + " " : "  ";
					Console.Write(draw);
				}
			}
		}
	}
}