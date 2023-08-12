using Xunit;

namespace Demo.Tests
{
    public class AssertingExceptionsTests
    {
        [Fact]
        public void Calculator_Divide_MustReturnErrorDivideByZero()
        {
            // Arrange
            var calculator = new Calculator();

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
        }

        [Fact]
        public void Employee_Salary_MustReturnErrorSalaryLessThanAllowed()
        {
            // Arrange & Act & Assert
            var exception =
                Assert.Throws<Exception>(() => EmployeeFactory.Create("Michael", 250));

            Assert.Equal("Salary less than allowed", exception.Message);
        }
    }
}
