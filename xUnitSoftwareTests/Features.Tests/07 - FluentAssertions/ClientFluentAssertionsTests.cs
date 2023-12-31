﻿using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Features.Tests
{
    [Collection(nameof(ClientAutoMockerColletion))]
    public class ClientFluentAssertionsTests
    {
        private readonly ClientTestsAutoMockerFixture _clientTestsAutoMockerFixture;
        private readonly ITestOutputHelper _outputHelper;

        public ClientFluentAssertionsTests(ClientTestsAutoMockerFixture clientTestsAutoMockerFixture, ITestOutputHelper outputHelper)
        {
            _clientTestsAutoMockerFixture = clientTestsAutoMockerFixture;
            _outputHelper = outputHelper;
        }

        [Fact(DisplayName = "New valid client")]
        [Trait("Category", "Client Fluent Assertion Tests")]
        public void Client_NewClient_MustBeValid()
        {
            // Arrange
            var client = _clientTestsAutoMockerFixture.GenerateValidNewClient();

            // Act
            var result = client.IsValid();

            // Assert 
            //Assert.True(result);
            //Assert.Equal(0, client.ValidationResult.Errors.Count);

            // Assert 
            result.Should().BeTrue();
            client.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "New invalid client")]
        [Trait("Category", "Client Fluent Assertion Tests")]
        public void Client_NewClient_MustBeInValid()
        {
            // Arrange
            var client = _clientTestsAutoMockerFixture.GenerateInvalidClient();

            // Act
            var result = client.IsValid();

            // Assert 
            //Assert.False(result);
            //Assert.NotEqual(0, client.ValidationResult.Errors.Count);

            // Assert 
            result.Should().BeFalse();
            client.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1,"Must have at least 1 error");

            _outputHelper.WriteLine($"{client.ValidationResult.Errors.Count} Errors were found");
        }
    }
}
