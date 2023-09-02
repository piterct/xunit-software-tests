using Xunit;

namespace Features.Tests.FluentAssertions
{
    [Collection(nameof(ClientAutoMockerColletion))]
    public class ClientFluentAssertionsTests
    {
        private readonly ClientTestsAutoMockerFixture _clientTestsAutoMockerFixture;

        public ClientFluentAssertionsTests(ClientTestsAutoMockerFixture clientTestsAutoMockerFixture)
        {
            _clientTestsAutoMockerFixture = clientTestsAutoMockerFixture;   
        }
    }
}
