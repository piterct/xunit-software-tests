using Features.Core;
using FluentAssertions;
using Xunit;

namespace Features.Tests
{
    public class CpfValidationTests
    {
        [Theory(DisplayName = "Valids Cpfs")]
        [Trait("Category", "CPF validation Tests")]
        [InlineData("15231766607")]
        [InlineData("78246847333")]
        [InlineData("64184957307")]
        [InlineData("21681764423")]
        [InlineData("13830803800")]
        public void Cpf_ValidateMultiplesNumbers_AllMustBeValid(string cpf)
        {
            // Assert
            var cpfValidation = new CpfValidation();

            // Act & Assert
            cpfValidation.IsValid(cpf).Should().BeTrue();
        }
    }
}
