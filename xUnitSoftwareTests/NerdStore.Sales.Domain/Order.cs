namespace NerdStore.Sales.Domain
{
    public class Order
    {
        public Order()
        {
            _itemsOrder = new List<ItemOrder>();
        }
        public decimal TotalValue { get; private set; }
        public List<ItemOrder> _itemsOrder { get; private set; }
        public IReadOnlyCollection<ItemOrder> ItemsOrder => _itemsOrder;
        public void AddItem(ItemOrder itemOrder)
        {
            _itemsOrder.Add(itemOrder);
            TotalValue = ItemsOrder.Sum(o => o.Quantity * o.UnitaryValue);
        }
    }
}
