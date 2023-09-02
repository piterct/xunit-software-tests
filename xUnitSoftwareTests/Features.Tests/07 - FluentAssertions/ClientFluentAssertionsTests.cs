using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientAutoMockerColletion))]
    public class ClientFluentAssertionsTests
    {
        private readonly ClientTestsAutoMockerFixture _clientTestsAutoMockerFixture;

        public ClientFluentAssertionsTests(ClientTestsAutoMockerFixture clientTestsAutoMockerFixture)
        {
            _clientTestsAutoMockerFixture = clientTestsAutoMockerFixture;   
        }

        [Fact(DisplayName = "New valid client")]
        [Trait("Category", "Client Fluent Assertion Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = _clientTestsAutoMockerFixture.GenerateValidNewClient();

            // Act
            var result = client.IsValid();

            // Assert 
            Assert.True(result);
            Assert.Equal(0, client.ValidationResult.Errors.Count);
        }
    }
}
