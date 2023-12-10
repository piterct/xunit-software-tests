using MediatR;
using Moq;
using Moq.AutoMock;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Domain;
using NerdStore.Sales.Domain.Repository;
using System.Net.Sockets;
using Xunit;
using static NerdStore.Sales.Domain.Order;

namespace NerdStore.Sales.Application.Tests.Commands.Order
{
    public class OrderCommandHandlerTests
    {
        private readonly Guid _clientId;
        private readonly Guid _productId;
        private readonly AutoMocker _mocker;
        private readonly OrderCommandHandler _orderCommandHandler;

        public OrderCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _orderCommandHandler = _mocker.CreateInstance<OrderCommandHandler>();

            _clientId = Guid.NewGuid();
            _productId = Guid.NewGuid();
        }

        [Fact(DisplayName = "Add new item order successful")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem__NewOrder__MustExecuteSuccessful()
        {
            // Arrange
            var orderCommand = new AddItemOrderCommand(Guid.NewGuid(), Guid.NewGuid(), "Test Product", 2, 100);

            _mocker.GetMock<IOrderRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _orderCommandHandler.Handle(orderCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.Add(It.IsAny<Domain.Order>()), Times.Once());
            _mocker.GetMock<IOrderRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once());
            //mocker.GetMock<IMediator>().Verify(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once());

        }

        [Fact(DisplayName = "Add new order item draft with successful")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem__NewOrderItemToDraftOrder__MustExecuteSuccessful()
        {
            //Arrange 
            var clientId = Guid.NewGuid();

            var order = OrderFactory.NewOrderDraft(clientId);
            var existOrderItem = new OrderItem(Guid.NewGuid(), "Test Product", 2, 100);
            order.AddItem(existOrderItem);

            var orderCommand = new AddItemOrderCommand(clientId, Guid.NewGuid(), "Expensive Product", 2, 100);

            _mocker.GetMock<IOrderRepository>()
                .Setup(r => r.GetDraftOrderByClientId(clientId)).Returns(Task.FromResult(order));
            _mocker.GetMock<IOrderRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _orderCommandHandler.Handle(orderCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.AddItem(It.IsAny<OrderItem>()), Times.Once());
            _mocker.GetMock<IOrderRepository>().Verify(r => r.Update(It.IsAny<Domain.Order>()), Times.Once());
            _mocker.GetMock<IOrderRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once());
        }


        [Fact(DisplayName = "Add exist order item draft with successful")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem__ExistOrderItemToDraftOrder__MustExecuteSuccessful()
        {
            //Arrange
            var clientId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var order = Domain.Order.OrderFactory.NewOrderDraft(clientId);
            var existOrderItem = new OrderItem(productId, "Random Product", 2, 100);
            order.AddItem(existOrderItem);


            var orderCommand = new AddItemOrderCommand(clientId, productId, "Random Product", 2, 100);

            _mocker.GetMock<IOrderRepository>()
                .Setup(r => r.GetDraftOrderByClientId(clientId)).Returns(Task.FromResult(order));
            _mocker.GetMock<IOrderRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _orderCommandHandler.Handle(orderCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.UpdateItem(It.IsAny<OrderItem>()), Times.Once());
            _mocker.GetMock<IOrderRepository>().Verify(r => r.Update(It.IsAny<Domain.Order>()), Times.Once());
            _mocker.GetMock<IOrderRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once());
        }

        [Fact(DisplayName = "Add  item  invalid command")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem__InvalidCommand__MustReturnFalseAndToThrowEventNotification()
        {
            //Arrange
            var orderCommand = new AddItemOrderCommand(Guid.Empty, Guid.Empty, "", 0, 0);

            //Act
            var result = await _orderCommandHandler.Handle(orderCommand, CancellationToken.None);

            //Assert
            Assert.False(result);
            _mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(5));
        }
    }
}
