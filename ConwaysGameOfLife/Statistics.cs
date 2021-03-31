using System;

namespace ConwaysGameOfLife
{
	class Statistics
	{
		public int Generation = 0;
		public int PopulationSize = 0;
		public int Change = 0;

		public void Print()
		{
			Console.WriteLine();
			Console.WriteLine("Generation: " + Generation);
			Console.WriteLine("Population Size: " + PopulationSize + "   ");
			Console.WriteLine("Change: " + Change + "   ");
		}
	}
}