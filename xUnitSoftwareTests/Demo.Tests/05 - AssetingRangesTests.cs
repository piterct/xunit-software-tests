using Xunit;

namespace Demo.Tests
{
    public class AssetingRangesTests
    {
        [Theory]
        [InlineData(700)]
        [InlineData(1500)]
        [InlineData(2000)]
        [InlineData(7500)]
        [InlineData(8000)]
        [InlineData(15000)]
        public void Employee_Salary_RangeSalaryMustBeLevelProfession(double salario)
        {
            // Arrange & Act
            var employee = new Employee("Michael", salario);

            // Assert
            if (employee.JobLevel == EJobLevel.Junior)
                Assert.InRange(employee.Salary, 500, 1999);

            if (employee.JobLevel == EJobLevel.Middle)
                Assert.InRange(employee.Salary, 2000, 7999);

            if (employee.JobLevel == EJobLevel.Senior)
                Assert.InRange(employee.Salary, 8000, double.MaxValue);

            Assert.NotInRange(employee.Salary, 0, 499);
        }
    }
}
