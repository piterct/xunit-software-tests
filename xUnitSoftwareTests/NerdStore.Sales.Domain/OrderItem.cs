﻿namespace NerdStore.Sales.Domain
{
    public class OrderItem
    {
        public OrderItem(Guid productId, string productName, int quantity, decimal unitaryValue)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitaryValue = unitaryValue;
        }

        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitaryValue { get; private set; }


        internal void AddUnit(int unit)
        {
            Quantity += unit;
        }

        internal decimal CalculateValue()
        {
            return Quantity * UnitaryValue;
        }
    }
}
