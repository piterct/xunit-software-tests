using NerdStore.WebApp.MVC;

namespace NerdStore.WebApp.Tests
{
    [CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]

    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupWebTests>> { }
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }
    public class IntegrationTestsFixture<IStartup> : IDisposable where IStartup : class
    {
        public void Dispose()
        {
        }
    }
}
