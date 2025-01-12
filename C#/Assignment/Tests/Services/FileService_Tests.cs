using Infrastructure.Services;
using Xunit;

public class FileService_Tests
{
  private readonly string _testDirectory;
  private readonly string _testFile;

  public FileService_Tests()
  {
    _testDirectory = Path.Combine(Path.GetTempPath(), "FileServiceTests");
    _testFile = Path.Combine(_testDirectory, "testfile.txt");

    if (Directory.Exists(_testDirectory))
    {
      Directory.Delete(_testDirectory, true);
    }
  }

  [Fact]
  public void GetTextFromFile_ShouldReturnContent_WhenFileExists()
  {
    // Arrange
    Directory.CreateDirectory(_testDirectory);
    File.WriteAllText(_testFile, "Test Content");

    var fileService = new FileService(_testDirectory, "testfile.txt");

    // Act
    var result = fileService.GetTextFromFile();

    // Assert
    Assert.Equal("Test Content", result);
  }

  [Fact]
  public void GetTextFromFile_ShouldReturnNull_WhenFileDoesNotExist()
  {
    // Arrange
    var fileService = new FileService(_testDirectory, "nonexistentfile.txt");

    // Act
    var result = fileService.GetTextFromFile();

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void SaveTextToFile_ShouldReturnTrue_WhenDirectoryExists()
  {
    // Arrange
    Directory.CreateDirectory(_testDirectory);

    var fileService = new FileService(_testDirectory, "testfile.txt");

    // Act
    var result = fileService.SaveTextToFile("Test Content");

    // Assert
    Assert.True(result);
    Assert.True(File.Exists(_testFile));
    Assert.Equal("Test Content", File.ReadAllText(_testFile));
  }

  [Fact]
  public void SaveTextToFile_ShouldCreateDirectoryAndSaveFile()
  {
    // Arrange
    var fileService = new FileService(_testDirectory, "testfile.txt");

    // Act
    var result = fileService.SaveTextToFile("Test Content");

    // Assert
    Assert.True(result);
    Assert.True(Directory.Exists(_testDirectory));
    Assert.Equal("Test Content", File.ReadAllText(_testFile));
  }
}
