using Bogus;
using Bogus.DataSets;
using Features.Clients;
using Xunit;

namespace Features.Tests._04___Human_Data
{
    [CollectionDefinition(nameof(ClientBogusCollection))]
    public class ClientBogusCollection : ICollectionFixture<ClientBogusTestsFixture>
    { }
    public  class ClientBogusTestsFixture
    {
        public Client GenerateValidNewClient()
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
    }
}
