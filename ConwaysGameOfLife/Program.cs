namespace ConwaysGameOfLife
{
	class Program
	{
		private static void Main()
		{
			Menus.Initialise();
			var userConfig = UserConfig.GetUserInput();
			var gameOfLife = new GameOfLife(userConfig);
			gameOfLife.Initialise();
			gameOfLife.Run();
		}
	}
}