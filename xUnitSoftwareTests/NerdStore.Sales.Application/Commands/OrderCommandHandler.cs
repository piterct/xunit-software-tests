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
            _orderRepository.Add(Order.OrderFactory.NewOrderDraft(message.ClientId));

            await _mediator.Publish(new AddedOrderItemEvent(), cancellationToken);

            return true;
        }
    }
}
