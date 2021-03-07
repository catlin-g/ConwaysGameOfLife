using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
	class Statistics
	{
		public int Generation = 0;
		public int PopulationSize = 0;

		public void Print()
		{
			Console.WriteLine();
			Console.WriteLine("Generation: " + Generation);
			Console.WriteLine("Population Size: " + PopulationSize + "     ");
		}
	}
}
