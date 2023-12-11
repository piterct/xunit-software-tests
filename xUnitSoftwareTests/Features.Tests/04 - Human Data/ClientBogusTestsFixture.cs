using Bogus;
using Bogus.DataSets;
using Features.Clients;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClientBogusCollection))]
    public class ClientBogusCollection : ICollectionFixture<ClientBogusTestsFixture>
    { }
    public class ClientBogusTestsFixture : IDisposable
    {
        private readonly Guid _clientId;
        public ClientBogusTestsFixture()
        {
            _clientId = Guid.NewGuid();
        }
        public Client GenerateValidNewClient()
        {
            return GenerateClients(1, true).FirstOrDefault();
        }

        public IEnumerable<Client> GetRandomClients()
        {
            var clients = new List<Client>();

            clients.AddRange(GenerateClients(50, true).ToList());
            clients.AddRange(GenerateClients(50, false).ToList());

            return clients;

        }

        public IEnumerable<Client> GenerateClients(int quantity, bool active)
        {

            var gender = new Faker().PickRandom<Name.Gender>();

            var clients = new Faker<Client>("pt_BR")
                .CustomInstantiator(f => new Client(
                    _clientId,
                    f.Name.FirstName(gender),
                    f.Name.LastName(gender),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    "",
                    active,
                    DateTime.Now))
                .RuleFor(c => c.Email, (f, c) =>
                f.Internet.Email(c.Name.ToLower(), c.LasName.ToLower()));


            return clients.Generate(quantity);
        }

        public Client GenerateInvalidClient()
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var client = new Faker<Client>("pt_BR")
                .CustomInstantiator(f => new Client(
                    Guid.NewGuid(),
                    f.Name.FirstName(gender),
                    f.Name.LastName(gender),
                    f.Date.Past(1, DateTime.Now.AddDays(-1)),
                    "",
                    false,
                    DateTime.Now));

            return client;
        }

        public void Dispose()
        {
        }
    }
}
