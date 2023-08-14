using Xunit;

namespace Demo.Tests
{
    public class AssertingColletionsTests
    {
        [Fact]
        public void Employee_Skills_MustNotHasEmptySkills()
        {
            // Arrange & Act
            var employe = EmployeeFactory.Create("Michael", 10000);

            // Assert
            Assert.All(employe.Skills, skill => Assert.False(string.IsNullOrWhiteSpace(skill)));
        }

        [Fact]
        public void Employee_Skills_JuniorMustHasBasicSkill()
        {
            // Arrange & Act
            var employe = EmployeeFactory.Create("Michael", 1000);

            // Assert
            Assert.Contains("OOP", employe.Skills);
        }

        [Fact]
        public void Employee_Skills_JuniorMustNotHasAdvancedSkill()
        {
            // Arrange & Act
            var employe = EmployeeFactory.Create("Michael", 1000);

            // Assert
            Assert.DoesNotContain("Microservices", employe.Skills);
        }

        [Fact]
        public void Employee_Skills_SeniorMustHasAllSkills()
        {
            // Arrange & Act
            var employe = EmployeeFactory.Create("Michael", 15000);

            var basicSkills = new[]
            {
                "Programming logic",
                "OOP",
                "Tests",
                "Microservices"
            };

            // Assert
            Assert.Equal(basicSkills, employe.Skills);
        }
    }
}
