using MediatR;
using NerdStore.Core.DomainObjects;
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
            if (!message.IsValid())
            {
                foreach(var error in message.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(message.MessageType, error.ErrorMessage), cancellationToken);
                }

                return false;
            }

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);
            var orderItem = new OrderItem(message.ProductId, message.Name, message.Quantity, message.UnitValue);

            if (order == null)
            {
                order = Order.OrderFactory.NewOrderDraft(message.ClientId);
                order.AddItem(orderItem);
                _orderRepository.Add(order);
            }
            else
            {
                var orderItemExist = order.ExistsOrderItem(orderItem);
                order.AddItem(orderItem);

                if (orderItemExist)
                {
                    _orderRepository.UpdateItem(order.OrderItems.First(p => p.ProductId == orderItem.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(orderItem);
                }

                _orderRepository.Update(order);
            }

            order.AddEvent(new AddedOrderItemEvent(order.ClientId, order.Id, message.ProductId,
                message.Name, message.UnitValue, message.Quantity));

            return await _orderRepository.UnitOfWork.Commit();
        }
    }
}
