using NerdStore.WebApp.MVC;
using NerdStore.WebApp.Tests.Config;

namespace NerdStore.WebApp.Tests
{
    public class UserTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _integrationTestsFixture;

        public UserTests(IntegrationTestsFixture<StartupWebTests> integrationTestsFixture)
        {
            _integrationTestsFixture = integrationTestsFixture;
        }
        [Fact(DisplayName = "Complete registration successfully")]
        [Trait("Category", "Integration Web - User")]
        public async Task User_RegistrationUser_MustBeSuccessful()
        {
            //Arrange
            
        }
    }
}
