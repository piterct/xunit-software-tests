namespace NerdStore.Sales.Domain
{
    public class ItemOrder
    {
        public ItemOrder(Guid productId, string productName, int quantity, decimal unitaryValue)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitaryValue = unitaryValue;
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitaryValue { get; set; }
    }
}
