using System;
using System.IO;
using System.Threading;

namespace ConwaysGameOfLife
{
	class GameOfLife
	{
		private readonly Cells cellsA;
		private readonly Cells cellsB;

		private Cells cellsDraw;
		private Cells cellsUpdate;

		private readonly int bufferSize = 5;
		private readonly int totalCells;

		private readonly Statistics statistics;
		private readonly UserConfig settings;

		private const int SleepTime = 200;

		private static readonly Random random = new Random();

		public GameOfLife(UserConfig settings)
		{
			cellsA = new Cells(settings.Width, settings.Height, settings.Buffer ? bufferSize : 0, settings.Wrap);
			cellsB = new Cells(settings.Width, settings.Height, settings.Buffer ? bufferSize : 0, settings.Wrap);
			cellsDraw = cellsA;
			cellsUpdate = cellsB;

			statistics = new Statistics();

			totalCells = settings.Width * settings.Height;

			this.settings = settings;
		}

		private static bool ShouldBeAlive(float chanceOfBeingAlive)
			=> random.NextDouble() < chanceOfBeingAlive;

		private void GenerateRandomSeed(Cells cells, float prosperous)
		{
			for (var y = 0; y < cells.Height; ++y)
			{
				for (var x = 0; x < cells.Width; ++x)
				{
					var value = ShouldBeAlive(prosperous);

					cells.SetValue(x, y, value);
				}
			}
		}

		private void DrawCurrentGeneration()
			=> cellsDraw.DrawConsole();

		private void DrawGUI()
			=> statistics.Print();

		private bool GenerateNextGeneration()
		{
			cellsUpdate.Update(cellsDraw);

			var temp = cellsDraw;
			cellsDraw = cellsUpdate;
			cellsUpdate = temp;

			statistics.generation++;
			statistics.populationSize = cellsDraw.GetPopulationCount();
			statistics.percentAlive = (float)cellsDraw.GetPopulationCount() / totalCells * 100f;
			statistics.change = cellsDraw.GetPopulationCount() - cellsUpdate.GetPopulationCount();

			return true;
		}

		private void LoadState()
		{
			if (File.Exists(settings.Path()))
			{
				var totalLines = File.ReadAllLines(settings.Path());
				if (totalLines.Length > 0)
				{
					var cellsPreset = new Cells(totalLines[0].Length, totalLines.Length, settings.Buffer ? bufferSize : 0, settings.Wrap);
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

					var val = (totalLines[0].Length == cellsDraw.Width) && (totalLines.Length == cellsDraw.Height);

					cellsDraw = val
						? cellsPreset
						: Translate(cellsDraw, cellsPreset);
				}
				else
				{
					throw new ArgumentException("File is empty.");
				}
			}
			else
			{
				throw new ArgumentException("File does not exist.");
			}
		}

		private Cells Translate(Cells canvas, Cells toPaste)
		{
			for (var y = 0; y < toPaste.Height; ++y)
			{
				for (var x = 0; x < toPaste.Width; ++x)
				{
					canvas.SetValue(x, y, toPaste.GetValue(x, y));
				}
			}
			return canvas;
		}

		public void Initialise()
		{
			Console.Clear();
			PrintMenus.RemoveConsoleFlicker();

			if (settings.Preset)
			{
				LoadState();
			}
			else if (settings.Random)
			{
				GenerateRandomSeed(cellsDraw, settings.Prosperity);
			}
		}

		public void Run()
		{
			do
			{
				PrintMenus.RemoveConsoleFlicker();
				DrawCurrentGeneration();
				DrawGUI();
				Thread.Sleep(SleepTime);
			}
			while (GenerateNextGeneration());
		}
	}
}