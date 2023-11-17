using MediatR;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain;
using NerdStore.Sales.Domain.Repository;

namespace NerdStore.Sales.Application.Commands
{
    public class OrderCommandHandler: IRequest<AddItemOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public bool Handle(AddItemOrderCommand message)
        {
            _orderRepository.Add(Order.OrderFactory.NewOrderDraft(message.ClientId));

            _mediator.Publish(new AddedOrderItemEvent());

            return true;
        }
    }
}
