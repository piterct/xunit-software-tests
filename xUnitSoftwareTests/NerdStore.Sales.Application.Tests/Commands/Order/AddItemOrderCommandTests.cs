using Xunit;

namespace NerdStore.Sales.Application.Tests.Commands.Order
{
    public class AddItemOrderCommandTests
    {
        [Fact(DisplayName = "Add Item valid command ")]
        [Trait("Category", "Sales - Order Commands")]
        public void AddItemOrderCommand__CommandMustBeValid__MustWorkAtValidation()
        {
            // Arrange
            var oderItemCommand = new AddItemOrderCommand(Guid.NewGuid(), Guid.NewGuid(), "Test Product", 2, 100);

            // Act
            var result = oderItemCommand.isValid();

            // Assert
            Assert.True(result);

        }
    }
}
