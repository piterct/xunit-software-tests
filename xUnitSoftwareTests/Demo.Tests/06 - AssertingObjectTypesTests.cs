﻿using Xunit;

namespace Demo.Tests
{
    public class AssertingObjectTypesTests
    {
        [Fact]
        public void EmployeeFactory_Create_MustReturnEmployeeType()
        {
            // Arrange & Act
            var employee = EmployeeFactory.Create("Michael", 10000);

            // Assert
            Assert.IsType<Employee>(employee);
        }

        [Fact]
        public void EmployeeFactory_Create_MustReturnEmployeeFromPerson()
        {
            // Arrange & Act
            var employee = EmployeeFactory.Create("Michael", 10000);

            // Assert
            Assert.IsAssignableFrom<Person>(employee);
        }
    }
}
