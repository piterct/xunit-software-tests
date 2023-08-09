using Xunit;

namespace Demo.Tests
{
    public class AssertNumbers
    {
        [Fact]
        public void Calculator_Sum_MustBeEqual()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Sum(1, 2);

            // Assert
            Assert.Equal(3, result);
        }
    }
}
