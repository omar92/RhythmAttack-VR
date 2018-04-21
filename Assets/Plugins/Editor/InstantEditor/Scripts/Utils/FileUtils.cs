namespace DeadMosquito.InstantEditor
{
	using System.IO;

	public static class FileUtils
	{
		public static bool IsFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return false;
			}

			var attr = File.GetAttributes(filePath);
			return (attr & FileAttributes.Directory) != FileAttributes.Directory;
		}
	}
}