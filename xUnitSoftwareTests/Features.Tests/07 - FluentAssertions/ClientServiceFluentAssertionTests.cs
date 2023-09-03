using Features.Clients;
using MediatR;
using Moq.AutoMock;
using Moq;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientAutoMockerColletion))]
    public class ClientServiceFluentAssertionTests
    {
        private readonly ClientTestsAutoMockerFixture _clientTestsAutoMockerFixture;
        private readonly ClientService _clientService;

        public ClientServiceFluentAssertionTests(ClientTestsAutoMockerFixture clientTestsAutoMockerFixture)
        {
            _clientTestsAutoMockerFixture = clientTestsAutoMockerFixture;
            _clientService = _clientTestsAutoMockerFixture.GetClientService();
        }

        [Fact(DisplayName = "Add Client Successful")]
        [Trait("Category", "Client Service Fluent Assertion Tests")]
        public void ClientService_Add_MustExecuteSuccessful()
        {
            // Arrange
            var client = _clientTestsAutoMockerFixture.GenerateValidNewClient();

            //Act
            _clientService.Add(client);

            // Assert
            Assert.True(client.IsValid());
            _clientTestsAutoMockerFixture.Mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Once);
            _clientTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }
    }
}
