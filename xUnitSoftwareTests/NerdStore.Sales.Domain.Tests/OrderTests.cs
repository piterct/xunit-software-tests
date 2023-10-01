using Xunit;

namespace NerdStore.Sales.Domain.Tests
{
    public class OrderTests
    {

        [Fact(DisplayName = "Add Item New Order ")]
        [Trait("Category", "Order Tests")]
        public void AddItemOrder__NewOrder_MustUpdateValue()
        {
            // Arrange
            var order = new Order();
            var itemOrder = new ItemOrder(Guid.NewGuid(), "Test Product", 2, 100);

            // Act
            order.Adicionar(itemOrder);

            // Assert
            Assert.Equal(200, order.TotalValue);
        }
    }
}
