namespace Infrastructure.Interfaces;

public interface IFileService
{
	bool SaveContentToFile(string content);
	string GetContentFromFile();
}