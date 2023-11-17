using MediatR;
using Moq;
using Moq.AutoMock;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Domain.Repository;
using Xunit;

namespace NerdStore.Sales.Application.Tests.Commands.Order
{
    public class OrderCommandHandlerTests
    {

        [Fact(DisplayName = "Add new item order successful")]
        [Trait("Category", "Sales - Order Command Handler")]
        public async Task AddItem__NewOrder__MustExecuteSuccessful()
        {
            // Arrange
            var orderCommand = new AddItemOrderCommand(Guid.NewGuid(), Guid.NewGuid(), "Test Product", 2, 100);

            var mocker = new AutoMocker();
            var orderHandler = mocker.CreateInstance<OrderCommandHandler>();

            // Act
            var result = await orderHandler.Handle(orderCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            mocker.GetMock<IOrderRepository>().Verify(r => r.Add(It.IsAny<Domain.Order>()), Times.Once());
            mocker.GetMock<IMediator>().Verify(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once());

        }
    }
}
