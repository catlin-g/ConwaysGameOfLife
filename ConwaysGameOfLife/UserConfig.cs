namespace ConwaysGameOfLife
{
	internal class UserConfig
	{
		public UserConfig(bool isRandom = true, bool isPreset = false, string PresetPath = @"Spaceship\Glider", bool useHard = true, bool useBuffer = false, bool useWrap = false, float prosperity = 0.50f)
		{
			Random = isRandom;
			Preset = isPreset;
			this.PresetPath = PresetPath;
			Hard = useHard;
			Buffer = useBuffer;
			Wrap = useWrap;
			Prosperity = prosperity;
		}

		public bool Random { get; set; }

		public bool Preset { get; set; }

		public string PresetPath { get; set; }

		private const string DataLocation = @"..\..\Data\";
		private const string FileType = ".txt";

		public string PathName() => $"{DataLocation}{PresetPath}{FileType}";

		public bool Hard { get; }

		public bool Buffer { get; }

		public bool Wrap { get; }

		public float Prosperity { get; set; }
	}
}