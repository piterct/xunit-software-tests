﻿using Xunit;

namespace Demo.Tests
{
    public class AssertNullBoolTests
    {
        [Fact]
        public void Employee_Name_MustNotBeNullOrEmpty()
        {
            // Arrange & Act
            var employee = new Employee("", 1000);

            // Assert
            Assert.False(string.IsNullOrEmpty(employee.Name));
        }

        [Fact]
        public void Employee_NickName_NotHasNickName()
        {
            // Arrange & Act
            var employee = new Employee("Michael", 1000);

            // Assert
            Assert.Null(employee.NickName);

            // Assert Bool
            Assert.True(string.IsNullOrEmpty(employee.NickName));
            Assert.False(employee.NickName?.Length > 0);
        }
    }
}
