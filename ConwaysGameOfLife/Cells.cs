﻿using System;

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

		public Cells(int width, int height, int buffer, bool wrap)
		{
			this.buffer = buffer;
			this.Wrap = wrap;
			cells = new bool[height + (buffer * 2), width + (buffer * 2)];
		}

		public int GetTotalCells() => Width * Height;

		public void SetValue(int x, int y, bool value) => cells[GetCoordinate(y, Height), GetCoordinate(x, Width)] = value;

		public bool GetValue(int x, int y) => cells[GetCoordinate(y, Height), GetCoordinate(x, Width)];

		private int GetCoordinate(int dividend, int divisor) => Wrap ? ModuloCore(dividend, divisor) : dividend;

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
		}
	}
}