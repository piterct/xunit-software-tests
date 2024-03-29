﻿using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Sales.Domain.Tests
{
    public class OrderTests
    {
        private readonly Order _order;
        private readonly Guid _productId;
        public OrderTests()
        {
            _order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());
            _productId = Guid.NewGuid();
        }

        [Fact(DisplayName = "Add New Order Item ")]
        [Trait("Category", "Sales - Order")]
        public void AddOrderItem__NewOrder_MustUpdateValue()
        {
            // Arrange
            var orderItem = new OrderItem(_productId, "Test Product", 2, 100);

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
            var orderItem = new OrderItem(_productId, "Test Product", 2, 100);
            _order.AddItem(orderItem);

            var orderItem2 = new OrderItem(_productId, "Test Product", 1, 100);

            // Act
            _order.AddItem(orderItem2);

            // Assert
            Assert.Equal(300, _order.TotalValue);
            Assert.Equal(1, _order.OrderItems.Count);
            Assert.NotNull(_order.OrderItems.FirstOrDefault());
            Assert.Equal(3, _order.OrderItems?.FirstOrDefault(p => p.ProductId == _productId)?.Quantity);
        }


        [Fact(DisplayName = "Add order item above allowable")]
        [Trait("Category", "Sales - Order")]
        public void AddOrderItem__ItemAboveAllowable__MustReturnException()
        {
            // Arrange
            var orderItem = new OrderItem(_productId, "Test Product", Order.MAX_UNITS_ITEM + 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => _order.AddItem(orderItem));

        }

        [Fact(DisplayName = "Add existing order item above allowable")]
        [Trait("Category", "Sales - Order ")]
        public void AddOrderItem__ExistingItemUnitAndSumAboveAllowable__MustReturnException()
        {
            // Arrange
            var orderItem = new OrderItem(_productId, "Test Product", 1, 100);
            var orderItem2 = new OrderItem(_productId, "Test Product", Order.MAX_UNITS_ITEM, 100);
            _order.AddItem(orderItem);

            // Act & Assert
            Assert.Throws<DomainException>(() => _order.AddItem(orderItem2));

        }

        [Fact(DisplayName = "Update non-exist order item")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__ItemDoesNotExistsInTheList__MustReturnException()
        {
            // Arrange
            var orderItem = new OrderItem(_productId, "Test Product", 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => _order.UpdateItem(orderItem));

        }

        [Fact(DisplayName = "Update valid order item")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__OrderItemValid__MustUpdateQuantity()
        {
            // Arrange
            var orderItem = new OrderItem(_productId, "Test Product", 2, 100);
            _order.AddItem(orderItem);
            var orderItemUpdated = new OrderItem(_productId, "Test Product", 5, 100);
            var newQuantity = orderItemUpdated.Quantity;

            //Act
            _order.UpdateItem(orderItemUpdated);

            //Assert
            Assert.NotNull(_order.OrderItems.FirstOrDefault());
            Assert.Equal(newQuantity, _order.OrderItems.FirstOrDefault(p => p.ProductId == _productId)?.Quantity);

        }

        [Fact(DisplayName = "Update order item validate total")]
        [Trait("Category", "Sales - Order ")]
        public void UpdateOrderItem__OrderWithDifferenceProducts__MustUpdateTotalValue()
        {
            // Arrange
            var orderItemExist1 = new OrderItem(Guid.NewGuid(), "Test Product", 2, 100);
            var orderItemExist2 = new OrderItem(_productId, "Test Product", 3, 15);
            _order.AddItem(orderItemExist1);
            _order.AddItem(orderItemExist2);

            var orderItemUpdated = new OrderItem(_productId, "Test Product", 5, 15);
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
            var orderItemExist1 = new OrderItem(_productId, "Test Product", 3, 15);
            _order.AddItem(orderItemExist1);

            var orderItemUpdated = new OrderItem(_productId, "Test Product", Order.MAX_UNITS_ITEM, 15);

            // Act & Assert
            Assert.Throws<DomainException>(() => _order.UpdateItem(orderItemUpdated));

        }

        [Fact(DisplayName = "Remove  unexist order item")]
        [Trait("Category", "Sales - Order ")]
        public void RemoveOrderItem__ItemDoesNotExistsInTheList__MustReturnException()
        {
            // Arrange
            var orderItemRemove = new OrderItem(_productId, "Test Product", 5, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => _order.RemoveItem(orderItemRemove));

        }

        [Fact(DisplayName = "Remove order item must calculate Total Value")]
        [Trait("Category", "Sales - Order ")]
        public void RemoveOrderItem__ItemExistsInTheList__MustUpdateTotalValue()
        {
            // Arrange
            var orderItem1 = new OrderItem(Guid.NewGuid(), "Xpto Product", 2, 100);
            var orderItem2 = new OrderItem(_productId, "Xpto Product", 3, 15);
            _order.AddItem(orderItem1);
            _order.AddItem(orderItem2);

            var orderTotalValue = orderItem2.Quantity * orderItem2.UnitValue;

            // Act & Assert
            _order.RemoveItem(orderItem1);

            //Assert
            Assert.Equal(orderTotalValue, _order.TotalValue);
        }

        [Fact(DisplayName = "Apply valid voucher")]
        [Trait("Category", "Sales - Order ")]
        public void Order__AplyValidVoucher__MustReturnWithoutErrors()
        {
            // Arrange
            var voucher = new Voucher("OFF-15", null, 15, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(15),
                true, false);

            // Act
            var result = _order.ApplyVoucher(voucher);

            //Assert
            Assert.True(result.IsValid);
        }


        [Fact(DisplayName = "Apply invalid voucher")]
        [Trait("Category", "Sales - Order ")]
        public void Order__AplyInvalidValidVoucher__MustReturnWithErrors()
        {
            // Arrange
            var voucher = new Voucher("OFF-15", null, 15, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(-1),
                true, true);

            // Act
            var result = _order.ApplyVoucher(voucher);

            //Assert
            Assert.False(result.IsValid);

        }

        [Fact(DisplayName = "Apply  voucher type of discount value")]
        [Trait("Category", "Sales - Order ")]
        public void AplyVoucher__VoucherTypeOfDiscountValue_MustDiscountOfValueTotal()
        {
            // Arrange
            var orderItem1 = new OrderItem(_productId, "Product Xpto", 2, 100);
            var orderItem2 = new OrderItem(_productId, "Product Test", 3, 15);
            _order.AddItem(orderItem1);
            _order.AddItem(orderItem2);

            var voucher = new Voucher("OFF-15", null, 15, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(10),
                true, false);

            var valueWithDiscount = _order.TotalValue - voucher.DiscountValue;

            // Act
            _order.ApplyVoucher(voucher);

            //Assert
            Assert.Equal(valueWithDiscount, _order.TotalValue);

        }

        [Fact(DisplayName = "Apply type of voucher discount percentage")]
        [Trait("Category", "Sales - Order ")]
        public void AplyVoucher__VoucherTypeOfDiscountPercentage_MustDiscountOfValueTotal()
        {
            // Arrange
            var orderItem1 = new OrderItem(_productId, "Product Xpto", 2, 100);
            var orderItem2 = new OrderItem(_productId, "Product Test", 3, 15);
            _order.AddItem(orderItem1);
            _order.AddItem(orderItem2);

            var voucher = new Voucher("OFF-15", 15, null, 1, ETypeOfDiscountVoucher.Percentage, DateTime.Now.AddDays(10),
                true, false);

            var valueDiscount = (_order.TotalValue * voucher.DiscountPercentage) / 100;
            var valueTotalWithDiscount = _order.TotalValue - valueDiscount;

            // Act
            _order.ApplyVoucher(voucher);

            //Assert
            Assert.Equal(valueTotalWithDiscount, _order.TotalValue);

        }

        [Fact(DisplayName = "Apply discount voucher exceeds order total value")]
        [Trait("Category", "Sales - Order ")]
        public void AplyVoucher__DiscountExceedsOrderTotalValue_OrderMustHasZeroValue()
        {
            // Arrange
            var orderItem1 = new OrderItem(_productId, "Product Xpto", 2, 100);
            _order.AddItem(orderItem1);

            var voucher = new Voucher("OFF-15", null, 300, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(10),
                true, false);

            // Act
            _order.ApplyVoucher(voucher);

            //Assert
            Assert.Equal(0, _order.TotalValue);
        }


        [Fact(DisplayName = "Apply recalculate discount when order modified")]
        [Trait("Category", "Sales - Order ")]
        public void AplyVoucher__ModifiedItemsOrder__MustRecalculateDiscountTotalValue()
        {
            // Arrange
            var orderItem1 = new OrderItem(_productId, "Xpto Product ", 2, 100);
            _order.AddItem(orderItem1);

            var voucher = new Voucher("OFF-15", null, 300, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(10),
                true, false);

            _order.ApplyVoucher(voucher);

            var orderItem2 = new OrderItem(_productId, "Test Product", 4, 25);

            // Act
            _order.AddItem(orderItem2);

            //Assert
            var expectedValue = _order.OrderItems.Sum(i => i.Quantity * i.UnitValue) - voucher.DiscountValue;
            Assert.Equal(expectedValue, _order.TotalValue);

        }
    }
}

