using NerdStore.Sales.Application.Commands;
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
            var result = oderItemCommand.IsValid();

            // Assert
            Assert.True(result);

        }

        [Fact(DisplayName = "Add Item invalid command")]
        [Trait("Categoria", "Sales - Order Commands")]
        public void AddItemOrderCommand_CommandMustBeInvalid_MustNotPassAtValidation()
        {
            // Arrange
            var orderCommand = new AddItemOrderCommand(Guid.Empty,
                Guid.Empty, "", 0, 0);

            // Act
            var result = orderCommand.IsValid();

            // Assert
            Assert.False(result);
            Assert.Contains(AddItemOrderCommandValidation.ClientIdErrorMsg, orderCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AddItemOrderCommandValidation.ProductIdErrorMsg, orderCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AddItemOrderCommandValidation.NameErrorMsg, orderCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AddItemOrderCommandValidation.QtdMinErrorMsg, orderCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AddItemOrderCommandValidation.ValueErrorMsg, orderCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
