using MediatR;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain;
using NerdStore.Sales.Domain.Repository;

namespace NerdStore.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddItemOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }


        public async Task<bool> Handle(AddItemOrderCommand message, CancellationToken cancellationToken)
        {
            var orderItem = new OrderItem(message.ProductId, message.Name, message.Quantity, message.UnitValue);
            var order = Order.OrderFactory.NewOrderDraft(message.ClientId);
            order.AddItem(orderItem);

            _orderRepository.Add(order);

            order.AddEvent(new AddedOrderItemEvent(order.ClientId, order.Id, message.ProductId,
                message.Name, message.UnitValue, message.Quantity));

            return await _orderRepository.UnitOfWork.Commit();
        }
    }
}
