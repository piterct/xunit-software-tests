using FluentValidation.Results;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain
{
    public class Order
    {
        public static int MAX_UNITS_ITEM => 15;
        public static int MIN_UNITS_ITEM => 1;
        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Guid ClientId { get; private set; }

        public decimal TotalValue { get; private set; }
        public EOrderStatus OrderStatus { get; private set; }
        public List<OrderItem> _orderItems { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public bool UsedVoucher { get; private set; }
        public Voucher Voucher { get; private set; }

        public ValidationResult ApplyVoucher(Voucher voucher)
        {
            var result = voucher.ValidateIfIsApplicable();
            if (!result.IsValid) return result;

            Voucher = voucher;
            UsedVoucher = true;

            CalculateDiscountValueTotal();

            return result;
        }

        public void CalculateDiscountValueTotal()
        {
            if (!UsedVoucher) return;

            if (Voucher.TypeOfDiscountVoucher == ETypeOfDiscountVoucher.Value)
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    TotalValue -= Voucher.DiscountValue.Value;
                }
            }
            else
            {
                if (Voucher.DiscountPercentage.HasValue)
                {
                    TotalValue -= (TotalValue * Voucher.DiscountPercentage.Value) / 100;
                }
            }
        }

        private void CalculateValueOrder()
        {
            TotalValue = OrderItems.Sum(o => o.CalculateValue());
        }

        public bool ExistsOrderItem(OrderItem item)
        {
            return _orderItems.Any(p => p.ProductId == item.ProductId);
        }

        public void ValidateItemOrderExist(OrderItem item)
        {
            if (!ExistsOrderItem(item)) throw new DomainException($"The item does not belong in the order");
        }

        private void ValidateOrderItemQuantityAllowable(OrderItem item)
        {
            var quantityItems = item.Quantity;
            if (ExistsOrderItem(item))
            {
                var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);
                quantityItems += existingItem.Quantity;
            }

            if (quantityItems > MAX_UNITS_ITEM) throw new DomainException($"Maximum of {MAX_UNITS_ITEM} units per product");
        }

        public void AddItem(OrderItem orderItem)
        {
            ValidateOrderItemQuantityAllowable(orderItem);

            if (ExistsOrderItem(orderItem))
            {
                var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId);

                existingItem.AddUnit(orderItem.Quantity);
                orderItem = existingItem;
                _orderItems.Remove(existingItem);
            }

            _orderItems.Add(orderItem);
            CalculateValueOrder();
        }

        public void UpdateItem(OrderItem orderItem)
        {
            ValidateItemOrderExist(orderItem);
            ValidateOrderItemQuantityAllowable(orderItem);

            var existItem = OrderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId);

            _orderItems.Remove(existItem);
            _orderItems.Add(orderItem);

            CalculateValueOrder();
        }

        public void RemoveItem(OrderItem orderItem)
        {
            ValidateItemOrderExist(orderItem);

            _orderItems.Remove(orderItem);

            CalculateValueOrder();
        }

        public void SetDraft()
        {
            OrderStatus = EOrderStatus.Draft;
        }

        public static class OrderFactory
        {
            public static Order NewOrderDraft(Guid clientId)
            {
                var order = new Order
                {
                    ClientId = clientId,
                };

                order.SetDraft();
                return order;
            }
        }

    }
}
