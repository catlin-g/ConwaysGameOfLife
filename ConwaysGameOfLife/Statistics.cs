using System;

namespace ConwaysGameOfLife
{
	class Statistics
	{
		public int generation = 0;
		public int populationSize = 0;
		public float percentAlive = 0f;
		public int change = 0;

		public void Print()
		{
			Console.WriteLine();
			Console.WriteLine("Generation: " + generation + "   ");
			Console.WriteLine("Population Size: " + populationSize + "   ");
			Console.WriteLine("Percent Alive: " + percentAlive + "%   ");
			Console.WriteLine("Change: " + change + "   ");
		}
	}
}