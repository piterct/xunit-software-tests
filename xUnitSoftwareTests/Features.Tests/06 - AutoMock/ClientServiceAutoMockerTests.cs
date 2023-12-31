﻿using Features.Clients;
using MediatR;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Features.Tests
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
        [Trait("Category", "Client Service Auto Mock Tests")]
        public void ClientService_Add_MustFailBecauseInvalidClient()
        {
            // Arrange
            var client = _clientBogusTestsFixture.GenerateInvalidClient();
            var mocker = new AutoMocker();
            var clientService = mocker.CreateInstance<ClientService>();


            //Act
            clientService.Add(client);

            // Assert
            Assert.False(client.IsValid());
            mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Never);
            mocker.GetMock<IMediator>().Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "GetAllActive Clients")]
        [Trait("Category", "Client Service Auto Mock Tests")]
        public void ClientService_GetAllActiveClients_MustReturnOnlyActiveClients()
        {
            // Arrange
            var mocker = new AutoMocker();
            var clientService = mocker.CreateInstance<ClientService>();

            mocker.GetMock<IClientRepository>().Setup(c => c.GetAll())
                .Returns(_clientBogusTestsFixture.GetRandomClients());


            // Act
            var clients = clientService.GetAllActive();

            // Assert
            mocker.GetMock<IClientRepository>().Verify(r => r.GetAll(), Times.Once);
            Assert.True(clients.Any());
            Assert.False(clients.Count(c => !c.Active) > 0);

        }
    }
}
