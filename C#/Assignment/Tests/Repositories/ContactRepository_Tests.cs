using System.Text.Json;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Moq;
using Xunit;

public class ContactRepository_Tests
{
  private readonly Mock<IFileService> _fileServiceMock;
  private readonly ContactRepository _contactRepository;

  public ContactRepository_Tests()
  {
    _fileServiceMock = new Mock<IFileService>();
    _contactRepository = new ContactRepository(_fileServiceMock.Object);
  }

  [Fact]
  public void SaveContactsToFile_ShouldReturnTrue_WhenSaveSucceeds()
  {
    // Arrange
    var contacts = new List<Contact> { new Contact { FirstName = "Test Name" } };
    _fileServiceMock.Setup(fs => fs.SaveTextToFile(It.IsAny<string>())).Returns(true);

    // Act
    var result = _contactRepository.SaveContactsToFile(contacts);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void SaveContactsToFile_ShouldReturnFalse_WhenSaveFails()
  {
    // Arrange
    var contacts = new List<Contact> { new Contact { FirstName = "Test Name" } };
    _fileServiceMock.Setup(fs => fs.SaveTextToFile(It.IsAny<string>())).Returns(false);

    // Act
    var result = _contactRepository.SaveContactsToFile(contacts);

    // Assert
    Assert.False(result);
  }

  [Fact]
  public void SaveContactsToFile_ShouldReturnFalse_OnException()
  {
    // Arrange
    var contacts = new List<Contact>();
    _fileServiceMock.Setup(fs => fs.SaveTextToFile(It.IsAny<string>())).Throws(new Exception("Test Exception"));

    // Act
    var result = _contactRepository.SaveContactsToFile(contacts);

    // Assert
    Assert.False(result);
  }

  [Fact]
  public void GetContactsFromFile_ShouldReturnContactsList_WhenJsonIsValid()
  {
    // Arrange
    var contacts = new List<Contact> { new Contact { FirstName = "Test Name" } };
    string json = JsonSerializer.Serialize(contacts);
    _fileServiceMock.Setup(fs => fs.GetTextFromFile()).Returns(json);

    // Act
    var result = _contactRepository.GetContactsFromFile();

    // Assert
    Assert.NotNull(result);
    Assert.Single(result);
    Assert.Equal("Test Name", result[0].FirstName);
  }

  [Fact]
  public void GetContactsFromFile_ShouldReturnEmptyList_WhenFileIsEmpty()
  {
    // Arrange
    _fileServiceMock.Setup(fs => fs.GetTextFromFile()).Returns(string.Empty);

    // Act
    var result = _contactRepository.GetContactsFromFile();

    // Assert
    Assert.Empty(result);
  }

  [Fact]
  public void GetContactsFromFile_ShouldReturnEmptyList_WhenJsonIsInvalid()
  {
    // Arrange
    _fileServiceMock.Setup(fs => fs.GetTextFromFile()).Returns("Invalid JSON");

    // Act
    var result = _contactRepository.GetContactsFromFile();

    // Assert
    Assert.Empty(result);
  }

  [Fact]
  public void GetContactsFromFile_ShouldReturnEmptyList_OnException()
  {
    // Arrange
    _fileServiceMock.Setup(fs => fs.GetTextFromFile()).Throws(new Exception("Test Exception"));

    // Act
    var result = _contactRepository.GetContactsFromFile();

    // Assert
    Assert.Empty(result);
  }
}
