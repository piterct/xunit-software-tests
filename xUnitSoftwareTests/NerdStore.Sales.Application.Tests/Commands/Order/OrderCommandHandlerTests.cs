﻿using MediatR;
using Moq;
using Moq.AutoMock;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Domain;
using NerdStore.Sales.Domain.Repository;
using Xunit;

namespace NerdStore.Sales.Application.Tests.Commands.Order
{
    public class OrderCommandHandlerTests
    {
        private readonly Guid _clientId;
        private readonly Guid _productId;
        private readonly Guid _clientIdEmpty;
        private readonly Guid _productIdEmpty;
        private readonly Domain.Order _order;
        private readonly AutoMocker _mocker;
        private readonly OrderCommandHandler _orderCommandHandler;

        public OrderCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _orderCommandHandler = _mocker.CreateInstance<OrderCommandHandler>();

            _clientId = Guid.NewGuid();
            _productId = Guid.NewGuid();
            _clientIdEmpty = Guid.Empty;
            _productIdEmpty = Guid.Empty;

            _order = Domain.Order.OrderFactory.NewOrderDraft(_clientId);
        }

        [Fact(DisplayName = "Add new item order successful")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem__NewOrder__MustExecuteSuccessful()
        {
            // Arrange
            var orderCommand = new AddItemOrderCommand(_clientId, _productId, "Test Product", 2, 100);

            _mocker.GetMock<IOrderRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _orderCommandHandler.Handle(orderCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            _mocker.GetMock<IOrderRepository>().Verify(r => r.Add(It.IsAny<Domain.Order>()), Times.Once());
            _mocker.GetMock<IOrderRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once());
            _mocker.GetMock<IMediator>().Verify(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never());

        }

        [Fact(DisplayName = "Add new order item draft with successful")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem__NewOrderItemToDraftOrder__MustExecuteSuccessful()
        {
            //Arrange 
            var existOrderItem = new OrderItem(Guid.NewGuid(), "Test Product", 2, 100);
            _order.AddItem(existOrderItem);

            var orderCommand = new AddItemOrderCommand(_clientId, Guid.NewGuid(), "Expensive Product", 2, 100);

            _mocker.GetMock<IOrderRepository>()
                .Setup(r => r.GetDraftOrderByClientId(_clientId)).Returns(Task.FromResult(_order));
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
            var existOrderItem = new OrderItem(_productId, "Random Product", 2, 100);
            _order.AddItem(existOrderItem);

            var orderCommand = new AddItemOrderCommand(_clientId, _productId, "Random Product", 2, 100);

            _mocker.GetMock<IOrderRepository>()
                .Setup(r => r.GetDraftOrderByClientId(_clientId)).ReturnsAsync(await Task.FromResult(_order));
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
            var orderCommand = new AddItemOrderCommand(_clientIdEmpty, _productIdEmpty, "", 0, 0);

            //Act
            var result = await _orderCommandHandler.Handle(orderCommand, CancellationToken.None);

            //Assert
            Assert.False(result);
            _mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(5));
        }
    }
}
