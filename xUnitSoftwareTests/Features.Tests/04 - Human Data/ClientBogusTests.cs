using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientBogusCollection))]
    public class ClientBogusTests
    {
        private readonly ClientBogusTestsFixture _clientBogusTestsFixture;

        public ClientBogusTests(ClientBogusTestsFixture clientBogusTestsFixture)
        {
            _clientBogusTestsFixture = clientBogusTestsFixture;
        }

        [Fact(DisplayName = "New Valid Client")]
        [Trait("Category", "Client Bogus Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = _clientBogusTestsFixture.GenerateValidNewClient();

            // Act
            var result = client.IsValid();

            // Assert 
            Assert.True(result);
            Assert.Equal(0, client.ValidationResult.Errors.Count);
        }
    }
}
