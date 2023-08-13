using Xunit;

namespace Features.Tests._02___Fixtures
{
    [Collection(nameof(ClientCollection))]
    public class ClientTestInvalid
    {
        private readonly ClientTestsFixture _clientTestsFixture;

        public ClientTestInvalid(ClientTestsFixture clientTestsFixture)
        {
            _clientTestsFixture = clientTestsFixture;
        }

        [Fact(DisplayName = "New invalid client")]
        [Trait("Category", "Client Fixture Tests")]
        public void Client_NewClient_MustBeInValid()
        {
            // Arrange
            var client = _clientTestsFixture.GenerateInvalidClient();

            // Act
            var result = client.IsValid();

            // Assert 
            Assert.False(result);
            Assert.NotEqual(0, client.ValidationResult.Errors.Count);
        }
    }
}
