using Features.Clients;
using Xunit;

namespace Features.Tests._01___Traits
{
    public class ClientTests
    {

        [Fact(DisplayName = "New Client is valid")]
        [Trait("Category", "Client Trait Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var cliente = new Client(
                Guid.NewGuid(),
                "Michael",
                "Peter",
                DateTime.Now.AddYears(-30),
                "michael@edu.com",
                true,
                DateTime.Now);

            // Act
            var result = cliente.IsValid();

            // Assert 
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
