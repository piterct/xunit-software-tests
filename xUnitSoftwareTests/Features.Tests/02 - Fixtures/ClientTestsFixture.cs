using Bogus;
using Bogus.DataSets;
using Features.Clients;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClientCollection))]
    public class ClientCollection : ICollectionFixture<ClientTestsFixture>
    { }
    public class ClientTestsFixture : IDisposable
    {

        public Client GenerateValidClient()
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var client = new Faker<Client>("pt_BR")
                .CustomInstantiator(f => new Client(
                    Guid.NewGuid(),
                    f.Name.FirstName(gender),
                    f.Name.LastName(gender),
                    f.Date.Past(80, DateTime.Now.AddDays(-18)),
                    "",
                    true,
                    DateTime.Now))
                .RuleFor(c => c.Email, (f, c) =>
                f.Internet.Email(c.Name.ToLower(), c.LasName.ToLower()));
          
            return client;
        }

        public Client GenerateInvalidClient()
        {
            var client = new Client(
             Guid.NewGuid(),
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
