using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Sales.Domain.Tests
{
    public class OrderTests
    {
        private readonly Order _order;
        public OrderTests()
        {
            _order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
        }

        [Fact(DisplayName = "Add New Order Item ")]
        [Trait("Category", "Sales - Order")]
        public void AddOrderItem__NewOrder_MustUpdateValue()
        {
            // Arrange
            var orderItem = new OrderItem(Guid.NewGuid(), "Test Product", 2, 100);

            // Act
            _order.AddItem(orderItem);

            // Assert
            Assert.Equal(200, _order.TotalValue);
        }

        [Fact(DisplayName = "Add existing order item ")]
        [Trait("Category", "Sales - Order")]
        public void AddOrderItem__ExistingOrderItem_MustIncrementItemsAndSumValues()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", 2, 100);
            _order.AddItem(orderItem);

            var orderItem2 = new OrderItem(productId, "Test Product", 1, 100);

            // Act
            _order.AddItem(orderItem2);

            // Assert
            Assert.Equal(300, _order.TotalValue);
            Assert.Equal(1, _order.OrderItems.Count);
            Assert.Equal(3, _order.OrderItems.FirstOrDefault(P => P.ProductId == productId).Quantity);
        }


        [Fact(DisplayName = "Add order item above allowable")]
        [Trait("Category", "Sales - Order")]
        public void AddOrderItem__ItemAboveAllowable__MustReturnException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", Order.MAX_UNITS_ITEM + 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => _order.AddItem(orderItem));

        }

        [Fact(DisplayName = "Add existing order item above allowable")]
        [Trait("Category", "Sales - Order ")]
        public void AddOrderItem__ExistingItemUnitAndSumAboveAllowable__MustReturnException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", 1, 100);
            var orderItem2 = new OrderItem(productId, "Test Product", Order.MAX_UNITS_ITEM, 100);
            _order.AddItem(orderItem);

            // Act & Assert
            Assert.Throws<DomainException>(() => _order.AddItem(orderItem2));

        }

        [Fact(DisplayName = "Update non-exist order item")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__ItemDoesNotExistsInTheList__MustReturnException()
        {
            // Arrange
            var orderItem = new OrderItem(Guid.NewGuid(), "Test Product", 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => _order.UpdateItem(orderItem));

        }

        [Fact(DisplayName = "Update valid order item")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__OrderItemValid__MustUpdateQuantity()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var orderItem = new OrderItem(productId, "Test Product", 2, 100);
            _order.AddItem(orderItem);
            var orderItemUpdated = new OrderItem(productId, "Test Product", 5, 100);
            var newQuantity = orderItemUpdated.Quantity;

            //Act
            _order.UpdateItem(orderItemUpdated);

            //Assert
            Assert.Equal(newQuantity, _order.OrderItems.FirstOrDefault(p => p.ProductId == productId).Quantity);

        }

        [Fact(DisplayName = "Update order item validate total")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__OrderWithDifferenceProducts__MustUpdateTotalValue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var orderItemExist1 = new OrderItem(Guid.NewGuid(), "Test Product", 2, 100);
            var orderItemExist2 = new OrderItem(productId, "Test Product", 3, 15);
            _order.AddItem(orderItemExist1);
            _order.AddItem(orderItemExist2);

            var orderItemUpdated = new OrderItem(productId, "Test Product", 5, 15);
            var orderTotal = orderItemExist1.Quantity * orderItemExist1.UnitValue +
                             orderItemUpdated.Quantity * orderItemUpdated.UnitValue;

            //Act
            _order.UpdateItem(orderItemUpdated);

            //Assert
            Assert.Equal(orderTotal, _order.TotalValue);

        }

        [Fact(DisplayName = "Update order item unit above allowable")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__ItemUnitAboveAllowable__MustUpdateTotalValue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var orderItemExist1 = new OrderItem(productId, "Test Product", 3, 15);
            _order.AddItem(orderItemExist1);

            var orderItemUpdated = new OrderItem(productId, "Test Product", Order.MAX_UNITS_ITEM, 15);

            // Act & Assert
            Assert.Throws<DomainException>(() => _order.UpdateItem(orderItemUpdated));

        }

        [Fact(DisplayName = "Remove  unexist order item")]
        [Trait("Category", "Sales - Order ")]
        public void RemoveOrderItem__ItemDoesNotExistsInTheList__MustReturnException()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var orderItemRemove = new OrderItem(Guid.NewGuid(), "Test Product", 5, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => order.RemoveItem(orderItemRemove));

        }

        [Fact(DisplayName = "Remove order item must calculate Total Value")]
        [Trait("Category", "Sales - Order ")]
        public void RemoveOrderItem__ItemExistsInTheList__MustUpdateTotalValue()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var orderItem1 = new OrderItem(Guid.NewGuid(), "Xpto Product", 2, 100);
            var orderItem2 = new OrderItem(productId, "Xpto Product", 3, 15);
            order.AddItem(orderItem1);
            order.AddItem(orderItem2);

            var orderTotalValue = orderItem2.Quantity * orderItem2.UnitValue;

            // Act & Assert
            order.RemoveItem(orderItem1);

            //Assert
            Assert.Equal(orderTotalValue, order.TotalValue);

        }

        [Fact(DisplayName = "Apply valid voucher")]
        [Trait("Category", "Sales - Order ")]
        public void Order__AplyValidVoucher__MustReturnWithoutErrors()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var voucher = new Voucher("OFF-15", null, 15, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(15),
                true, false);

            // Act
            var result = order.ApplyVoucher(voucher);

            //Assert
            Assert.True(result.IsValid);

        }


        [Fact(DisplayName = "Apply invalid voucher")]
        [Trait("Category", "Sales - Order ")]
        public void Order__AplyInvalidValidVoucher__MustReturnWithErrors()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            var voucher = new Voucher("OFF-15", null, 15, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(-1),
                true, true);

            // Act
            var result = order.ApplyVoucher(voucher);

            //Assert
            Assert.False(result.IsValid);

        }

        [Fact(DisplayName = "Apply  voucher type of discount value")]
        [Trait("Category", "Sales - Order ")]
        public void AplyVoucher__VoucherTypeOfDiscountValue_MustDiscountOfValueTotal()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());

            var orderItem1 = new OrderItem(Guid.NewGuid(), "Product Xpto", 2, 100);
            var orderItem2 = new OrderItem(Guid.NewGuid(), "Product Test", 3, 15);
            order.AddItem(orderItem1);
            order.AddItem(orderItem2);

            var voucher = new Voucher("OFF-15", null, 15, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(10),
                true, false);

            var valueWithDiscount = order.TotalValue - voucher.DiscountValue;

            // Act
            order.ApplyVoucher(voucher);

            //Assert
            Assert.Equal(valueWithDiscount, order.TotalValue);

        }

        [Fact(DisplayName = "Apply type of voucher discount percentage")]
        [Trait("Category", "Sales - Order ")]
        public void AplyVoucher__VoucherTypeOfDiscountPercentage_MustDiscountOfValueTotal()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());

            var orderItem1 = new OrderItem(Guid.NewGuid(), "Product Xpto", 2, 100);
            var orderItem2 = new OrderItem(Guid.NewGuid(), "Product Test", 3, 15);
            order.AddItem(orderItem1);
            order.AddItem(orderItem2);

            var voucher = new Voucher("OFF-15", 15, null, 1, ETypeOfDiscountVoucher.Percentage, DateTime.Now.AddDays(10),
                true, false);

            var valueDiscount = (order.TotalValue * voucher.DiscountPercentage) / 100;
            var valueTotalWithDiscount = order.TotalValue - valueDiscount;

            // Act
            order.ApplyVoucher(voucher);

            //Assert
            Assert.Equal(valueTotalWithDiscount, order.TotalValue);

        }

        [Fact(DisplayName = "Apply discount voucher exceeds order total value")]
        [Trait("Category", "Sales - Order ")]
        public void AplyVoucher__DiscountExceedsOrderTotalValue_OrderMustHasZeroValue()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());

            var orderItem1 = new OrderItem(Guid.NewGuid(), "Product Xpto", 2, 100);
            order.AddItem(orderItem1);

            var voucher = new Voucher("OFF-15", null, 300, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(10),
                true, false);

            // Act
            order.ApplyVoucher(voucher);

            //Assert
            Assert.Equal(0, order.TotalValue);

        }


        [Fact(DisplayName = "Apply recalculate discount when order modified")]
        [Trait("Category", "Sales - Order ")]
        public void AplyVoucher__ModifiedItemsOrder__MustRecalculateDiscountTotalValue()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());

            var orderItem1 = new OrderItem(Guid.NewGuid(), "Xpto Product ", 2, 100);
            order.AddItem(orderItem1);

            var voucher = new Voucher("OFF-15", null, 300, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(10),
                true, false);

            order.ApplyVoucher(voucher);

            var orderItem2 = new OrderItem(Guid.NewGuid(), "Test Product", 4, 25);

            // Act
            order.AddItem(orderItem2);


            //Assert
            var expectedValue = order._orderItems.Sum(i => i.Quantity * i.UnitValue) - voucher.DiscountValue;
            Assert.Equal(expectedValue, order.TotalValue);

        }
    }
}

