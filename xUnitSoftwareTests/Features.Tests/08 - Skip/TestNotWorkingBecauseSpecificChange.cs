using Xunit;

namespace Features.Tests
{
    public class TestNotWorkingBecauseSpecificChange
    {
        [Fact(DisplayName = "New Client 2.0", Skip = "New version 2.0 not working")]
        [Trait("Category", "Skipping Tests")]
        public void Test_NotWorking_InCompatibleVersion()
        {
          Assert.True(false);
        }
    }
}
