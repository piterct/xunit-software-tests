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

            var antiForgeryToken =
                _integrationTestsFixture.GetAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            _integrationTestsFixture.GenerateUserPassword();

            var formData = new Dictionary<string, string>
            {
                {_integrationTestsFixture.AntiForgeryFieldName, antiForgeryToken },
                {"Input.Email", _integrationTestsFixture.UserEmail},
                {"Input.Password", _integrationTestsFixture.UserPassword},
                {"Input.ConfirmPassword", _integrationTestsFixture.UserPassword}
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
            Assert.Contains($"Hello {_integrationTestsFixture.UserEmail}!", responseString);
        }
    }
}
