using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Sales.Domain.Tests
{
    public class OrderItemTests
    {
        [Fact(DisplayName = "New order item with unit below allowable")]
        [Trait("Category", "Sales - Order Item")]
        public void AddOrderItem__ItemBelowAllowable__MustReturnException()
        {
            // Arrange  & Act & Assert
            Assert.Throws<DomainException>(() =>
                new OrderItem(Guid.NewGuid(), "Test Product", Order.MIN_UNITS_ITEM - 1, 100));
        }
    }
}
