﻿namespace NerdStore.Sales.Domain
{
    public class Order
    {
        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Guid ClientId { get; private set; }

        public decimal TotalValue { get; private set; }
        public EOrderStatus OrderStatus { get; private set; }
        public List<OrderItem> _orderItems { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void CalculateValueOrder()
        {
            TotalValue = OrderItems.Sum(o => o.CalculateValue());
        }

        public void AddItem(OrderItem itemOrder)
        {
            if (_orderItems.Any(p => p.ProductId == itemOrder.ProductId))
            {
                var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == itemOrder.ProductId);
                existingItem.AddUnit(itemOrder.Quantity);
                itemOrder = existingItem;

                _orderItems.Remove(existingItem);
            }

            _orderItems.Add(itemOrder);
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
