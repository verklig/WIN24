using System.Diagnostics;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class FileService : IFileService
{
	private readonly string _directoryPath;
	private readonly string _filePath;

	public FileService(string directoryPath, string fileName)
	{
		_directoryPath = directoryPath;
		_filePath = Path.Combine(_directoryPath, fileName);
	}

	public string GetTextFromFile()
	{
		if (File.Exists(_filePath)) 
		{
			return File.ReadAllText(_filePath);
		}
		
		return null!;
	}

	public bool SaveTextToFile(string content)
	{
		try 
		{
			if (!Directory.Exists(_directoryPath))
			{
				Directory.CreateDirectory(_directoryPath);
			}
			
			File.WriteAllText(_filePath, content);
			return true;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			return false;
		}
	}
}