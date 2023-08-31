using Bogus;
using Bogus.DataSets;
using Features.Clients;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClientAutoMockerColletion))]
    public class ClientAutoMockerColletion : ICollectionFixture<ClientTestsAutoMockerFixture>
    { }

    public class ClientTestsAutoMockerFixture : IDisposable
    {
        public Client GenerateValidNewClient()
        {
            return GenerateClients(1, true).FirstOrDefault();
        }

        public IEnumerable<Client> GenerateClients(int quantity, bool active)
        {

            var gender = new Faker().PickRandom<Name.Gender>();

            var clients = new Faker<Client>("pt_BR")
                .CustomInstantiator(f => new Client(
                    Guid.NewGuid(),
                    f.Name.FirstName(gender),
                    f.Name.LastName(gender),
                    f.Date.Past(80, DateTime.Now.AddDays(-18)),
                    "",
                    active,
                    DateTime.Now))
                .RuleFor(c => c.Email, (f, c) =>
                    f.Internet.Email(c.Name.ToLower(), c.LasName.ToLower()));


            return clients.Generate(quantity);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
