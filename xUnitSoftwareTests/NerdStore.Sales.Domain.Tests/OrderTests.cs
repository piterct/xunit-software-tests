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
            var order = new Order();
            var itemOrder = new OrderItem(Guid.NewGuid(), "Test Product", 2, 100);

            // Act
            order.AddItem(itemOrder);

            // Assert
            Assert.Equal(200, order.TotalValue);
        }

        [Fact(DisplayName = "Add existing order item ")]
        [Trait("Category", "Order Tests")]
        public void AddOrderItem__ExistingOrderItem_MustIncrementItemsAndSumValues()
        {
            // Arrange
           

            // Act

            // Assert
        }
    }
}
