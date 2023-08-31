using Xunit;

namespace Features.Tests._06___AutoMock
{
    [CollectionDefinition(nameof(ClientAutoMockerColletion))]
    public class ClientAutoMockerColletion : ICollectionFixture<ClientTestsAutoMockerFixture>
    { }

    public class ClientTestsAutoMockerFixture : IDisposable
    {


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
