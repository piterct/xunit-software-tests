using NerdStore.Sales.Domain;
using NerdStore.Sales.Domain.Repository;

namespace NerdStore.Sales.Application.Commands
{
    public class OrderCommandHandler
    {
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public bool Handle(AddItemOrderCommand message)
        {
            _orderRepository.Add(Order.OrderFactory.NewOrderDraft(message.ClientId));
            return true;
        }
    }
}
