using NerdStore.WebApp.MVC;
using NerdStore.WebApp.Tests.Config;

namespace NerdStore.WebApp.Tests
{
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _integrationTestsFixture;

        public UsuarioTests(IntegrationTestsFixture<StartupWebTests> integrationTestsFixture)
        {
            _integrationTestsFixture = integrationTestsFixture;
        }
    }
}
