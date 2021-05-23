using System;

namespace ConwaysGameOfLife
{
	class Statistics
	{
		public int Generation;
		public int PopulationSize;
		public float PercentAlive;
		public int Change;
		public const string fixConsolePrintIssue = "   ";

		public void Print()
		{
			Console.WriteLine();
			Console.WriteLine("Generation: " + Generation + fixConsolePrintIssue);
			Console.WriteLine("Population Size: " + PopulationSize + fixConsolePrintIssue);
			Console.WriteLine("Percent Alive: " + PercentAlive + "%" + fixConsolePrintIssue);
			Console.WriteLine("Change: " + Change + fixConsolePrintIssue);
		}
	}
}