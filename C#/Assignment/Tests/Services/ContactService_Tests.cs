using Infrastructure.Models;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Moq;
using Xunit;

public class ContactService_Tests
{
	private readonly Mock<IContactRepository> _contactRepositoryMock;
	private readonly ContactService _contactService;

	public ContactService_Tests()
	{
		_contactRepositoryMock = new Mock<IContactRepository>();
		_contactService = new ContactService(_contactRepositoryMock.Object);
	}

	[Fact]
	public void CreateContact_ShouldAddContact_WhenValidContact()
	{
		// Arrange
		var contact = new Contact
		{
			FirstName = "Test First Name",
			LastName = "Test Last Name",
			Email = "test@test.test",
			PhoneNumber = "1234567890",
			Street = "test",
			PostalCode = "12345",
			Locality = "test"
		};

		_contactRepositoryMock.Setup(cr => cr.SaveContactsToFile(It.IsAny<List<Contact>>())).Returns(true);
		_contactRepositoryMock.Setup(cr => cr.GetContactsFromFile()).Returns(new List<Contact>());

		// Act
		var result = _contactService.CreateContact(contact);

		// Assert
		Assert.True(result);
		_contactRepositoryMock.Verify(cr => cr.SaveContactsToFile(It.Is<List<Contact>>(l => l.Count == 1)), Times.Once);
	}

	[Fact]
	public void CreateContact_ShouldReturnFalse_WhenExceptionOccurs()
	{
		// Arrange
		var contact = new Contact { FirstName = "Test Name" };
		_contactRepositoryMock.Setup(cr => cr.SaveContactsToFile(It.IsAny<List<Contact>>())).Throws(new Exception("Test exception"));

		// Act
		var result = _contactService.CreateContact(contact);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void DeleteContact_ShouldDeleteContact_WhenValidIndex()
	{
		// Arrange
		var contacts = new List<Contact>
		{
			new Contact { Id = "1", FirstName = "Test Name" },
			new Contact { Id = "2", FirstName = "Test Name 2" }
		};

		_contactRepositoryMock.Setup(cr => cr.GetContactsFromFile()).Returns(contacts);
		_contactRepositoryMock.Setup(cr => cr.SaveContactsToFile(It.IsAny<List<Contact>>())).Returns(true);

		// Act
		var result = _contactService.DeleteContact("1");

		// Assert
		Assert.True(result);
		Assert.Single(contacts);
	}

	[Fact]
	public void DeleteContact_ShouldReturnFalse_WhenInvalidIndex()
	{
		// Arrange
		var contacts = new List<Contact>
		{
			new Contact { Id = "1", FirstName = "Test Name" }
		};

		_contactRepositoryMock.Setup(cr => cr.GetContactsFromFile()).Returns(contacts);

		// Act
		var result = _contactService.DeleteContact("invalid");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void DeleteContact_ShouldReturnFalse_WhenOutOfRangeIndex()
	{
		// Arrange
		var contacts = new List<Contact>
		{
			new Contact { Id = "1", FirstName = "Test Name" }
		};

		_contactRepositoryMock.Setup(cr => cr.GetContactsFromFile()).Returns(contacts);

		// Act
		var result = _contactService.DeleteContact("99");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void UpdateContact_ShouldUpdateExistingContact_WhenValidContact()
	{
		// Arrange
		var contact = new Contact { Id = "1", FirstName = "Test First Name", LastName = "Test Last Name" };
		var updatedContact = new Contact { Id = "1", FirstName = "Test First Name", LastName = "Test Last Name Edited" };

		var contacts = new List<Contact> { contact };
		_contactRepositoryMock.Setup(cr => cr.GetContactsFromFile()).Returns(contacts);
		_contactRepositoryMock.Setup(cr => cr.SaveContactsToFile(It.IsAny<List<Contact>>())).Returns(true);

		// Act
		_contactService.UpdateContact(updatedContact);

		// Assert
		Assert.Equal("Test Last Name Edited", contacts.First().LastName);
		_contactRepositoryMock.Verify(cr => cr.SaveContactsToFile(It.IsAny<List<Contact>>()), Times.Once);
	}

	[Fact]
	public void UpdateContact_ShouldNotUpdate_WhenContactNotFound()
	{
		// Arrange
		var contact = new Contact { Id = "1", FirstName = "Test First Name" };
		var updatedContact = new Contact { Id = "2", FirstName = "Test First Name Edited" };

		var contacts = new List<Contact> { contact };
		_contactRepositoryMock.Setup(cr => cr.GetContactsFromFile()).Returns(contacts);

		// Act
		_contactService.UpdateContact(updatedContact);

		// Assert
		Assert.Equal("Test First Name", contacts.First().FirstName);
	}

	[Fact]
	public void SaveContacts_ShouldReturnTrue_WhenSaveSucceeds()
	{
		// Arrange
		var contacts = new List<Contact> { new Contact { FirstName = "Test First Name" } };
		_contactRepositoryMock.Setup(cr => cr.SaveContactsToFile(It.IsAny<List<Contact>>())).Returns(true);

		// Act
		var result = _contactService.SaveContacts();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void GetAllContacts_ShouldReturnContacts()
	{
		// Arrange
		var contacts = new List<Contact> { new Contact { FirstName = "Test First Name" } };
		_contactRepositoryMock.Setup(cr => cr.GetContactsFromFile()).Returns(contacts);

		// Act
		var result = _contactService.GetAllContacts();

		// Assert
		Assert.Single(result);
	}
}