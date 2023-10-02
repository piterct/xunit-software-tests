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

        private void CalculateValueOrder()
        {
            TotalValue = OrderItems.Sum(o => o.CalculateValue());
        }

        public void AddItem(OrderItem itemOrder)
        {
            if (itemOrder.Quantity > MAX_UNITS_ITEM) throw new DomainException($"Maximum of {MAX_UNITS_ITEM} units per product");

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
