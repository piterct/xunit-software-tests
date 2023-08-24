using Features.Clients;
using MediatR;
using Moq;
using Xunit;

namespace Features.Tests.Mock
{
    [Collection(nameof(ClientBogusCollection))]
    public class ClientServiceTests
    {
        private readonly ClientBogusTestsFixture _clientBogusTestsFixture;

        public ClientServiceTests(ClientBogusTestsFixture clientBogusTestsFixture)
        {
            _clientBogusTestsFixture = clientBogusTestsFixture;
        }

        [Fact(DisplayName = "Add Client Successful")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_Add_MustExecuteSuccessful()
        {
            // Arrange
            var client = _clientBogusTestsFixture.GenerateValidNewClient();
            var clientRepo = new Mock<IClientRepository>();
            var mediator = new Mock<IMediator>();

            var clientService = new ClientService(clientRepo.Object, mediator.Object);

            //Act
            clientService.Add(client);

            // Assert
            Assert.True(client.IsValid());
            clientRepo.Verify(r => r.Add(client), Times.Once);
            mediator.Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add Client UnSuccessful")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_Add_MustFailBecauseInvalidClient()
        {
            // Arrange
            var client = _clientBogusTestsFixture.GenerateInvalidClient();
            var clientRepo = new Mock<IClientRepository>();
            var mediator = new Mock<IMediator>();

            var clientService = new ClientService(clientRepo.Object, mediator.Object);

            //Act
            clientService.Add(client);

            // Assert
            Assert.False(client.IsValid());
            clientRepo.Verify(r => r.Add(client), Times.Never);
            mediator.Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "GetAllActive Clients")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_GetAllActiveClients_MustReturnOnlyActiveClients()
        {
            // Arrange
            var clientRepo = new Mock<IClientRepository>();
            var mediator = new Mock<IMediator>();

            var clientService = new ClientService(clientRepo.Object, mediator.Object);

            // Act
            var clients = clientService.GetAllActive();

            // Assert
            clientRepo.Verify(r => r.GetAll(), Times.Once);
            Assert.True(clients.Any()); 
            Assert.False(clients.Count(c => !c.Active) > 0);

        }
    }
}
