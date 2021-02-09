namespace ConwaysGameOfLife
{
	class Program
	{
		static void Main(string[] args)
		{
			var gameOfLife = new GameOfLife();

			gameOfLife.Initialise();
			gameOfLife.Run();
		}
	}
}
