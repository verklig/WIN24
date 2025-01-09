using Infrastructure.Factories;
using Infrastructure.Models;
using Xunit;

namespace Tests.Factories;

public class ContactFactory_Tests
{
	[Fact]
	public void Create_ShouldReturnContact()
	{
		Contact result = ContactFactory.Create();

		Assert.NotNull(result);
		Assert.IsType<Contact>(result);
	}
}