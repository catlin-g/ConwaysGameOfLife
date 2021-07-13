namespace ConwaysGameOfLife
{
	internal class Driver
	{
		public Driver() => Start();

		public void Start()
		{
			var userConfig = Menu.StartMenu();

			var gameOfLife = new GameOfLife(userConfig);
			gameOfLife.Initialise();
			gameOfLife.Run();
		}
	}
}