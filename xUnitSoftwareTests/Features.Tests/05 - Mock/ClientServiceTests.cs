using Features.Clients;
using MediatR;
using Moq;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientBogusCollection))]
    public class ClientServiceTests
    {
        private readonly ClientBogusTestsFixture _clientBogusTestsFixture;
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;

        public ClientServiceTests(ClientBogusTestsFixture clientBogusTestsFixture)
        {
            _clientBogusTestsFixture = clientBogusTestsFixture;
            _clientRepositoryMock = new Mock<IClientRepository>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact(DisplayName = "Add Client Successful")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_Add_MustExecuteSuccessful()
        {
            // Arrange
            var client = _clientBogusTestsFixture.GenerateValidNewClient();
            var clientService = new ClientService(_clientRepositoryMock.Object, _mediatorMock.Object);

            //Act
            clientService.Add(client);

            // Assert
            Assert.True(client.IsValid());
            _clientRepositoryMock.Verify(r => r.Add(client), Times.Once);
            _mediatorMock.Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add Client UnSuccessful")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_Add_MustFailBecauseInvalidClient()
        {
            // Arrange
            var client = _clientBogusTestsFixture.GenerateInvalidClient();
            var clientService = new ClientService(_clientRepositoryMock.Object, _mediatorMock.Object);

            //Act
            clientService.Add(client);

            // Assert
            Assert.False(client.IsValid());
            _clientRepositoryMock.Verify(r => r.Add(client), Times.Never);
            _mediatorMock.Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "GetAllActive Clients")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_GetAllActiveClients_MustReturnOnlyActiveClients()
        {
            // Arrange
            _clientRepositoryMock.Setup(c => c.GetAll())
                .Returns(_clientBogusTestsFixture.GetRandomClients());

            var clientService = new ClientService(_clientRepositoryMock.Object, _mediatorMock.Object);

            // Act
            var clients = clientService.GetAllActive();

            // Assert
            _clientRepositoryMock.Verify(r => r.GetAll(), Times.Once);
            Assert.NotNull(clients);
            Assert.True(clients.Any());
            Assert.False(clients.Count(c => !c.Active) > 0);

        }
    }
}
