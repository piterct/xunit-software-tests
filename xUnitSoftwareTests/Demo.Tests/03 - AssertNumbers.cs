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

        [Fact]
        public void Calculator_Sum_MustBeNotEqual()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Somar(1.13123123123, 2.2312313123);

            // Assert
            Assert.NotEqual(3.3, result, 1);
        }
    }
}
