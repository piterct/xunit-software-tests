using Xunit;

namespace Demo.Tests
{
    public class AssertStringsTests
    {
        [Fact]
        public void StringsTools_NamesJoin_ReturnCompleteName()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var completeName = sut.Join("Michael", "Peter");

            // Assert
            Assert.Equal("Michael Peter", completeName);
        }


        [Fact]
        public void StringsTools_NameJoin_MustIgnoreCase()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var completeName = sut.Join("Eduardo", "Pires");

            // Assert
            Assert.Equal("EDUARDO PIRES", completeName, true);
        }

        [Fact]
        public void StringsTools_NamesJoin_MustContainsStretch()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var completeName = sut.Join("Michael", "Peter");

            // Assert
            Assert.Contains("ichael", completeName);
        }

        [Fact]
        public void StringsTools_NamesJoin_MustStartsWith()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var completeName = sut.Join("Michael", "Peter");

            // Assert
            Assert.StartsWith("Mich", completeName);
        }

        [Fact]
        public void StringsTools_NamesJoin_MustEndsWith()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var completeName = sut.Join("Michael", "Peter");

            // Assert
            Assert.EndsWith("ter", completeName);
        }

        [Fact]
        public void StringsTools_NamesJoin_ValidateRegularExpression()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var completeName = sut.Join("Michael", "Peter");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", completeName);
        }

    }
}
