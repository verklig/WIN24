using Infrastructure.Helpers;
using Xunit;

namespace Tests.Helpers;

public class IdGenerator_Tests
{
	[Fact]
	public void Generate_ShouldReturnGuidAsString()
	{
		string result = IdGenerator.GenerateId();

		Assert.NotNull(result);
		Assert.True(Guid.TryParse(result, out _));
	}
}