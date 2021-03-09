namespace ConwaysGameOfLife
{
	class Program
	{
		static void Main(string[] args)
		{
			var gameOfLife = new GameOfLife();

			gameOfLife.HandleUserInput();
			gameOfLife.Initialise();
			gameOfLife.Run();
		}
	}
}