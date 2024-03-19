using NerdStore.WebApp.MVC;
using NerdStore.WebApp.Tests.Config;

namespace NerdStore.WebApp.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
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
            var initialResponse = await _integrationTestsFixture.Client.GetAsync("/Identity/Account/Register");
            initialResponse.EnsureSuccessStatusCode();

            var email = "tests@teste.com";

            var formData = new Dictionary<string, string>
            {
                {"Input.Email", email},
                {"Input.Password", "Tests@123"},
                {"Input.ConfirmPassword", "Tests@123"}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Identity/Account/Register")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            //Act
            var postResponse = await _integrationTestsFixture.Client.SendAsync(postRequest);

            //Assert
            var responseString = await postResponse.Content.ReadAsStringAsync();

            postResponse.EnsureSuccessStatusCode();
            Assert.Contains($"Hello {email}!", responseString);

        }
    }
}
