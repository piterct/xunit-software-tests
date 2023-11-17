using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events
{
    public class AddedOrderItemEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string NameProduct { get; set; }
        public decimal UnitValue { get; private set; }
        public int Quantity { get; private set; }

        public AddedOrderItemEvent(Guid clientId, Guid orderId, Guid productId, string nameProduct, decimal unitValue, int quantity)
        {
                ClientId = clientId;
                OrderId = orderId;
                ProductId = productId;
                NameProduct = nameProduct;
                UnitValue = unitValue;
                Quantity = quantity;
        }
    }
}
