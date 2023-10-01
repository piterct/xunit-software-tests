using Xunit;

namespace NerdStore.Sales.Domain.Tests
{
    public class OrderTests
    {

        [Fact(DisplayName = "Add New Order Item ")]
        [Trait("Category", "Order Tests")]
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
        [Trait("Category", "Order Tests")]
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
            Assert.Equal(1,order.OrderItems.Count);
            Assert.Equal(3, order.OrderItems.FirstOrDefault(P=> P.ProductId == productId).Quantity);
        }


        [Fact(DisplayName = "Add order item above 15 items ")]
        [Trait("Category", "Order Tests")]
        public void AddOrderItem__ItemAbove15Units__MustReturnException()
        {
            // Arrange
            

            // Act

            // Assert

        }
    }
}
