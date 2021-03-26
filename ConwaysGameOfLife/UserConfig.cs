using System;

namespace ConwaysGameOfLife
{
	class UserConfig
	{
		private readonly bool isRandom;
		private readonly string path;
		private readonly bool useBuffer;
		private readonly bool useWrap;

		public UserConfig(bool isRandom, string path, bool useBuffer, bool useWrap)
		{
			this.isRandom = isRandom;
			this.path = path;
			this.useBuffer = useBuffer;
			this.useWrap = useWrap;
		}

		public bool GetUseRandom() => isRandom;

		public bool GetUseBuffer() => useBuffer;

		public string GetString()
		{
			var saveLocation = @"C:\Users\cgrange\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\Data\";
			var fileType = ".txt";
			return saveLocation + path + fileType;
		}

		public bool GetUsePreset() => !string.IsNullOrEmpty(path);

		public bool GetUseWrap() => useWrap;
	}
}
