using Features.Clients;
using Xunit;

namespace Features.Tests._01___Traits
{
    public class ClientTests
    {

        [Fact(DisplayName = "New valid client")]
        [Trait("Category", "Client Trait Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = new Client(
                Guid.NewGuid(),
                "Michael",
                "Peter",
                DateTime.Now.AddYears(-30),
                "michael@edu.com",
                true,
                DateTime.Now);

            // Act
            var result = client.IsValid();

            // Assert 
            Assert.True(result);
            Assert.Equal(0, client.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "New invalid client")]
        [Trait("Category", "Client Trait Tests")]
        public void Client_NewClient_MustBeInValid()
        {
            // Arrange
            var client = new Client(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "michael@edu.com",
                true,
                DateTime.Now);

            // Act
            var result = client.IsValid();

            // Assert 
            Assert.False(result);
            Assert.NotEqual(0, client.ValidationResult.Errors.Count);
        }
    }
}
