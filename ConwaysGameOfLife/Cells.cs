using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public Cells(int numberOfRows, int numberOfCols)
		{
			cells = new bool[numberOfRows, numberOfCols];
		}

		private readonly bool[,] cells;
		private int numOfCellsAlive;

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

		public int Rows => cells.GetLength(0);
		public int Cols => cells.GetLength(1);

		public void Update(Cells currentState)
		{
			numOfCellsAlive = 0;

			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
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

		private static int GetAliveNeighbours(int cellY, int cellX, Cells currentState)
		{
			var aliveNeighbours = 0;

			for (var y = cellY - 1; y < cellY + 2; y++)
			{
				var validRow = (y >= 0) && (y <= (currentState.Rows - 1));

				if (!validRow)
				{
					continue;
				}

				for (var x = cellX - 1; x < cellX + 2; x++)
				{
					var validCol = (x >= 0) && (x <= (currentState.Cols - 1));
					var thisCell = (y == cellY) && (x == cellX);

					if (validCol && !thisCell && currentState.GetValue(x, y))
					{
						aliveNeighbours++;
					}
				}
			}

			return aliveNeighbours;
		}

		public char AliveCellSymbol = '\u25A0';

		public void DrawConsole()
		{
			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
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
