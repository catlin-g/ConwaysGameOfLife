﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace ConwaysGameOfLife
{
	internal class GameOfLife
	{
		private bool[,] cells;
		private readonly bool isAlive = true;
		private int generation = 0;

		private readonly int size = 20;
		private readonly int row;
		private readonly int col;

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
			row = col = size;
			cells = new bool[row, col];
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
			var random = new Random();

			for (var i = 0; i < size; i++)
			{
				for (var j = 0; j < size; j++)
				{
					cells[i, j] = prosperous
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

			for (var i = 0; i < size; i++)
			{
				for (var j = 0; j < size; j++)
				{
					if (j == 0)
					{
						Console.WriteLine();
					}
					//var draw = cells[i, j] ? "(" + i + ", " + j + ")" : "(" + i + ", " + j + ")";
					var draw = cells[i, j] ? blackSquare + " " : "  ";
					Console.Write(draw);
				}
			}

			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Generation: " + generation);
		}

		/// <summary>
		/// Calculates the next generation via the following rules:
		///     Any live cell with two or three live neighbors survives.
		///     Any dead cell with three live neighbors becomes a live cell.
		///     All other live cells die in the next generation. Similarly, all other dead cells stay dead.
		/// </summary>	trailing
		private void GenerateNextGeneration()
		{
			var tmp = new bool[row, col];

			for (var i = 0; i < cells.GetLength(0); i++)
			{
				for (var j = 0; j < cells.GetLength(1); j++)
				{
					var totalNeighbours = GetAliveNeighbours(i, j);
					var checkNeighbours = (totalNeighbours == 2) || (totalNeighbours == 3);
					//Console.WriteLine("Cell: (" + i + ", " + j + ") Value:" + cells[i, j] + "Neighbors (Alive):" + GetNeighbours((i, j)).Count);

					if (cells[i, j] && checkNeighbours)
					{
						//Console.WriteLine("Cell: (" + i + ", " + j + ") Value:" + cells[i, j] + "Neighbors (Alive):" + GetNeighbours((i, j)).Count);
						tmp[i, j] = isAlive;
					}
					else if ((!cells[i, j]) && (totalNeighbours == 3))
					{
						tmp[i, j] = isAlive;
					}
					else
					{
						tmp[i, j] = !isAlive;
					}
				}
			}
			cells = tmp;
			generation++;
		}

		private int GetAliveNeighbours(int r, int c)
		{
			var aliveNeighbours = 0;

			for (var i = r - 1; i < r + 2; i++)
			{
				for (var j = c - 1; j < c + 2; j++)
				{
					var validRow = (i >= 0) && (i <= (row - 1));
					var validCol = (j >= 0) && (j <= (col - 1));
					var thisCell = (i == r) && (j == c);

					if (validRow && validCol && !thisCell && cells[i, j])
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

			Thread.Sleep(200);
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

				Thread.Sleep(200);
			}
		}
	}
}