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
		public int Height => cells.GetLength(0);
		public int Length => cells.GetLength(1);

		public Cells(int length, int height)
		{
			cells = new bool[length, height];
		}

		public void SetValue(int x, int y, bool value)
		{
			cells[x, y] = value;
		}

		public bool GetValue(int x, int y)
		{
			return cells[x, y];
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
				for (var x = 0; x < Length; x++)
				{
					var aliveNeighbours = GetAliveNeighbours(x, y, currentState);
					var checkNeighbours = (aliveNeighbours == 2) || (aliveNeighbours == 3);
					var cellAlive = currentState.GetValue(x, y);

					var isAlive = (cellAlive && checkNeighbours) || (!cellAlive && (aliveNeighbours == 3));
					SetValue(x, y, isAlive);

					if(isAlive)
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
					var validX = (x >= 0) && (x <= (currentState.Length - 1));
					var thisCell = (y == cellY) && (x == cellX);

					if (validX && !thisCell && currentState.GetValue(x, y))
					{
						aliveNeighbours++;
					}
				}
			}
			return aliveNeighbours;
		}

		public void DrawConsole()
		{
			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Length; x++)
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
