using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientCollection))]
    public class ClientTestValid
    {
        private readonly ClientTestsFixture _clientTestsFixture;

        public ClientTestValid(ClientTestsFixture clientTestsFixture)
        {
            _clientTestsFixture = clientTestsFixture;
        }

        [Fact(DisplayName = "New valid client")]
        [Trait("Category", "Client Fixture Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = _clientTestsFixture.GenerateValidClient();

            // Act
            var result = client.IsValid();

            // Assert 
            Assert.True(result);
            Assert.Equal(0, client.ValidationResult.Errors.Count);
        }
    }
}
