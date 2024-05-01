using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace BuildTasks;

public class WriteFilePaths : Microsoft.Build.Utilities.Task
{
	[Required]
	public string DirectoryPath { get; set; } = null!;

	[Required]
	public string OutputFilePath { get; set; } = null!;

	public override bool Execute()
	{
		try
		{
			var files = Directory.GetFiles(DirectoryPath, "*.json", SearchOption.TopDirectoryOnly);
			var fileNames = files.Select(Path.GetFileName).ToArray();

			// suppress warning cause andnoying
			File.WriteAllLines(OutputFilePath, fileNames!);
			return true;
		}
		catch (Exception ex)
		{
			Log.LogErrorFromException(ex);
			return false;
		}
	}
}