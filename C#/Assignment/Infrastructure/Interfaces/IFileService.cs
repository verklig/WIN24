namespace Infrastructure.Interfaces;

public interface IFileService
{
	bool SaveTextToFile(string content);
	string GetTextFromFile();
}