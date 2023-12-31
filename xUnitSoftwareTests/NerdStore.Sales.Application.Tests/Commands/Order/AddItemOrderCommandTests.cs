﻿using NerdStore.Sales.Application.Commands;
using Xunit;

namespace NerdStore.Sales.Application.Tests.Commands.Order
{
    public class AddItemOrderCommandTests
    {
        private readonly Guid _clientId;
        private readonly Guid _productId;
        private readonly Guid _clientIdEmpty;
        private readonly Guid _productIdEmpty;

        public AddItemOrderCommandTests()
        {
            _clientId = Guid.NewGuid();
            _productId = Guid.NewGuid();
            _clientIdEmpty = Guid.Empty;
            _productIdEmpty = Guid.Empty;

        }

        [Fact(DisplayName = "Add Item valid command ")]
        [Trait("Category", "Sales - Order Commands")]
        public void AddItemOrderCommand__CommandMustBeValid__MustWorkAtValidation()
        {
            // Arrange
            var oderItemCommand = new AddItemOrderCommand(_clientId, _productId, "Test Product", 2, 100);

            // Act
            var result = oderItemCommand.IsValid();

            // Assert
            Assert.True(result);

        }

        [Fact(DisplayName = "Add Item invalid command")]
        [Trait("Category", "Sales - Order Commands")]
        public void AddItemOrderCommand_CommandMustBeInvalid_MustNotPassAtValidation()
        {
            // Arrange
            var orderCommand = new AddItemOrderCommand(_clientIdEmpty,
                _productIdEmpty, "", 0, 0);

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

        [Fact(DisplayName = "Add Item Command units above allowed")]
        [Trait("Category", "Sales - Order Commands")]
        public void AddItemOrderCommand_UnitQuantityAboveAllowed_MustNotPassAtValidation()
        {
            // Arrange
            var pedidoCommand = new AddItemOrderCommand(_clientId,
                _productId, "Produto Teste", Domain.Order.MAX_UNITS_ITEM + 1, 100);

            // Act
            var result = pedidoCommand.IsValid();

            // Assert
            Assert.False(result);
            Assert.Contains(AddItemOrderCommandValidation.QtdMaxErrorMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
