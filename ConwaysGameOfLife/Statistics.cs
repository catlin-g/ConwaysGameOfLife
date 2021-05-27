using System;

namespace ConwaysGameOfLife
{
	class Statistics
	{
		public int generation = 0;
		public int populationSize = 0;
		public float percentAlive = 0f;
		public int change = 0;
		public const string fixConsolePrintIssue = "      ";

		public void Print()
		{
			Console.WriteLine();
			Console.WriteLine($"Generation: {generation}{fixConsolePrintIssue}");
			Console.WriteLine($"Population Size: {populationSize}{fixConsolePrintIssue}");
			Console.WriteLine($"Percent Alive: {percentAlive}%{fixConsolePrintIssue}");
			Console.WriteLine($"Change: {change}{fixConsolePrintIssue}");
		}
	}
}