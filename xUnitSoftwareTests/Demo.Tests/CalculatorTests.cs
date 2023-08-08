using Xunit;

namespace Demo.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Calculator_Sum_ReturnValueSum()
        {
            // Arrange
            var calculator = new Calculator();

            //Act
            var result = calculator.Sum(2, 2);

            //Assert
            Assert.Equal(4, result);
        }
    }
}
