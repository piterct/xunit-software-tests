﻿using Features.Clients;
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
            clientRepo.Verify(r=> r.Add(client), Times.Once);
            mediator.Verify(v=> v.Publish(It.IsAny<INotification>(),CancellationToken.None), Times.Once);
        }
    }
}
