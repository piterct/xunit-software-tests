using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Sales.Domain.Tests
{
    public class OrderTests
    {

        [Fact(DisplayName = "Add New Order Item ")]
        [Trait("Category", "Sales - Order")]
        public void AddOrderItem__NewOrder_MustUpdateValue()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var orderItem = new OrderItem(Guid.NewGuid(), "Test Product", 2, 100);

            // Act
            order.AddItem(orderItem);

            // Assert
            Assert.Equal(200, order.TotalValue);
        }

        [Fact(DisplayName = "Add existing order item ")]
        [Trait("Category", "Sales - Order")]
        public void AddOrderItem__ExistingOrderItem_MustIncrementItemsAndSumValues()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", 2, 100);
            order.AddItem(orderItem);

            var orderItem2 = new OrderItem(productId, "Test Product", 1, 100);

            // Act
            order.AddItem(orderItem2);

            // Assert
            Assert.Equal(300, order.TotalValue);
            Assert.Equal(1, order.OrderItems.Count);
            Assert.Equal(3, order.OrderItems.FirstOrDefault(P => P.ProductId == productId).Quantity);
        }


        [Fact(DisplayName = "Add order item above allowable")]
        [Trait("Category", "Sales - Order")]
        public void AddOrderItem__ItemAboveAllowable__MustReturnException()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", Order.MAX_UNITS_ITEM + 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.AddItem(orderItem));

        }

        [Fact(DisplayName = "Add existing order item above allowable")]
        [Trait("Category", "Sales - Order ")]
        public void AddOrderItem__ExistingItemUnitAndSumAboveAllowable__MustReturnException()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", 1, 100);
            var orderItem2 = new OrderItem(productId, "Test Product", Order.MAX_UNITS_ITEM, 100);
            order.AddItem(orderItem);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.AddItem(orderItem2));

        }

        [Fact(DisplayName = "Update non-exist order item")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__ItemDoesNotExistsInTheList__MustReturnException()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var orderItem = new OrderItem(Guid.NewGuid(), "Test Product", 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.UpdateItem(orderItem));

        }

        [Fact(DisplayName = "Update valid order item")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__OrderItemValid__MustUpdateQuantity()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", 2, 100);
            order.AddItem(orderItem);
            var orderItemUpdated = new OrderItem(productId, "Test Product", 5, 100);
            var newQuantity = orderItemUpdated.Quantity;

            //Act
            order.UpdateItem(orderItemUpdated);

            //Assert
            Assert.Equal(newQuantity, order.OrderItems.FirstOrDefault(p => p.ProductId == productId).Quantity);

        }

        [Fact(DisplayName = "Update order item validate total")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__OrderWithDifferenceProducts__MustUpdateTotalValue()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItemExist1 = new OrderItem(Guid.NewGuid(), "Test Product", 2, 100);
            var orderItemExist2 = new OrderItem(productId, "Test Product", 3, 15);
            order.AddItem(orderItemExist1);
            order.AddItem(orderItemExist2);

            var orderItemUpdated = new OrderItem(productId, "Test Product", 5, 15);
            var orderTotal = orderItemExist1.Quantity * orderItemExist1.UnitValue +
                             orderItemUpdated.Quantity * orderItemUpdated.UnitValue;

            //Act
            order.UpdateItem(orderItemUpdated);

            //Assert
            Assert.Equal(orderTotal, order.TotalValue);

        }

        [Fact(DisplayName = "Update order item unit above allowable")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__ItemUnitAboveAllowable__MustUpdateTotalValue()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItemExist1 = new OrderItem(productId, "Test Product", 3, 15);
            order.AddItem(orderItemExist1);

            var orderItemUpdated = new OrderItem(productId, "Test Product", Order.MAX_UNITS_ITEM, 15);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.UpdateItem(orderItemUpdated));

        }

        [Fact(DisplayName = "Remove  unexist order item unit ")]
        [Trait("Category", "Sales - Order ")]
        public void RemoveOrderItem__ItemDoesNotExistsInTheList__MustReturnException()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var orderItemRemove = new OrderItem(Guid.NewGuid(), "Test Product", 5, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.RemoveItem(orderItemRemove));

        }

    }
}
