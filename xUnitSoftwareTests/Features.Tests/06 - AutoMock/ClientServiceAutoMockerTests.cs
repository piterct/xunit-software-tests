using Features.Clients;
using MediatR;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Features.Tests.AutoMock
{
    [Collection(nameof(ClientBogusCollection))]
    public class ClientServiceAutoMockerTests
    {
        private readonly ClientBogusTestsFixture _clientBogusTestsFixture;
        public ClientServiceAutoMockerTests(ClientBogusTestsFixture clientBogusTestsFixture)
        {
            _clientBogusTestsFixture = clientBogusTestsFixture;
        }


        [Fact(DisplayName = "Add Client Successful")]
        [Trait("Category", "Client Service AutoMock Tests")]
        public void ClientService_Add_MustExecuteSuccessful()
        {
            // Arrange
            var client = _clientBogusTestsFixture.GenerateValidNewClient();
            var mocker = new AutoMocker();
            var clientService = mocker.CreateInstance<ClientService>();

            //Act
            clientService.Add(client);

            // Assert
            Assert.True(client.IsValid());
            mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Once);
            mocker.GetMock<IMediator>().Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add Client UnSuccessful")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_Add_MustFailBecauseInvalidClient()
        {
            // Arrange
            var client = _clientBogusTestsFixture.GenerateValidNewClient();
            var mocker = new AutoMocker();
            var clientService = mocker.CreateInstance<ClientService>();


            //Act
            clientService.Add(client);

            // Assert
            Assert.False(client.IsValid());
            mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Never);
            mocker.GetMock<IMediator>().Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }
    }
}
