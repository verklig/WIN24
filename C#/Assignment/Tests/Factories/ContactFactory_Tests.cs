using Infrastructure.Factories;
using Infrastructure.Models;
using Xunit;

namespace Tests.Factories;

public class ContactFactory_Tests
{
	[Fact]
	public void Create_ShouldReturnContact()
	{
		// Act
		Contact result = ContactFactory.Create();

		// Assert
		Assert.NotNull(result);
		Assert.IsType<Contact>(result);
	}
}