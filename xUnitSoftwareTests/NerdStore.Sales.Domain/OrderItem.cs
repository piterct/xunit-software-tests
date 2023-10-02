using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain
{
    public class OrderItem
    {
        public OrderItem(Guid productId, string productName, int quantity, decimal unitValue)
        {
            if (quantity < Order.MIN_UNITS_ITEM) throw new DomainException($"Minimum of {Order.MIN_UNITS_ITEM} units per product");

            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }


        internal void AddUnit(int unit)
        {
            Quantity += unit;
        }

        internal decimal CalculateValue()
        {
            return Quantity * UnitValue;
        }
    }
}
