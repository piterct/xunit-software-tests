using Features.Clients;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClientCollection))]
    public class ClientCollection : ICollectionFixture<ClientTestsFixture>
    { }
    public class ClientTestsFixture : IDisposable
    {
        private readonly Guid _clientId;
        public ClientTestsFixture()
        {
            _clientId = Guid.NewGuid();
        }

        public Client GenerateValidClient()
        {
            var client = new Client(
                _clientId,
           "Michael",
           "Peter",
           DateTime.Now.AddYears(-30),
           "michael@edu.com",
           true,
           DateTime.Now);

            return client;
        }

        public Client GenerateInvalidClient()
        {
            var client = new Client(
                _clientId,
             "",
             "",
             DateTime.Now,
             "michael@edu.com",
             true,
             DateTime.Now);

            return client;
        }

        public void Dispose()
        {

        }
    }
}
