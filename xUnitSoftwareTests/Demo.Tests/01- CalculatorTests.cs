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

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2, 2, 4)]
        [InlineData(4, 2, 6)]
        [InlineData(7, 3, 10)]
        [InlineData(6, 6, 12)]
        [InlineData(9, 9, 18)]
        public void Calculator_Sum_ReturnRightValuesSum(double v1, double v2, double total) 
        {
            // Arrange
            var calculator = new Calculator();

            //Act
            var result = calculator.Sum(v1, v2);

            Assert.Equal(total, result);

        }
    }
}
