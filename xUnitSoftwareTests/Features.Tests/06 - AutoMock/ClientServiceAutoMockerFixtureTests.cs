﻿using Features.Clients;
using MediatR;
using Moq;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientAutoMockerColletion))]
    public class ClientServiceAutoMockerFixtureTests
    {
        private readonly ClientTestsAutoMockerFixture _clientTestsAutoMockerFixture;
        private readonly ClientService _clientService;
        public ClientServiceAutoMockerFixtureTests(ClientTestsAutoMockerFixture clientTestsAutoMockerFixture)
        {
            _clientTestsAutoMockerFixture = clientTestsAutoMockerFixture;
            _clientService = _clientTestsAutoMockerFixture.GetClientService();
        }

        
        [Fact(DisplayName = "Add Client Successful")]
        [Trait("Category", "Client Service AutoMockFixture Tests")]
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

        [Fact(DisplayName = "Add Client UnSuccessful")]
        [Trait("Category", "Client Service AutoMockFixture Tests")]
        public void ClientService_Add_MustFailBecauseInvalidClient()
        {
            // Arrange
            var client = _clientTestsAutoMockerFixture.GenerateInvalidClient();

            //Act
            _clientService.Add(client);

            // Assert
            Assert.False(client.IsValid());
            _clientTestsAutoMockerFixture.Mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Never);
            _clientTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "GetAllActive Clients")]
        [Trait("Category", "Client Service AutoMockFixture Tests")]
        public void ClientService_GetAllActiveClients_MustReturnOnlyActiveClients()
        {
            // Arrange
            _clientTestsAutoMockerFixture.Mocker.GetMock<IClientRepository>().Setup(c => c.GetAll())
                .Returns(_clientTestsAutoMockerFixture.GetRandomClients());

            // Act
            var clients = _clientService.GetAllActive();

            // Assert
            _clientTestsAutoMockerFixture.Mocker.GetMock<IClientRepository>().Verify(r => r.GetAll(), Times.Once);
            Assert.True(clients.Any());
            Assert.False(clients.Count(c => !c.Active) > 0);

        }
    }
}
