namespace NerdStore.Sales.Application.Commands
{
    public class AddItemOrderCommand
    {
        public Guid ClientId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }

        public AddItemOrderCommand()
        {

        }

    }
}
