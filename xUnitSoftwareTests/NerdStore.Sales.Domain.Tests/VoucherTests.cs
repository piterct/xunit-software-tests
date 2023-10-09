using Xunit;

namespace NerdStore.Sales.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validate a type value valid voucher ")]
        [Trait("Category", "Sales - Voucher")]
        public void Voucher_ValidateATypeValueVoucher_MustBeValid()
        {
            // Arrange
            var voucher = new Voucher("OFF-15", null, 15, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(15),
                true, false);

            // Act
            var result = voucher.ValidateIfIsApplicable();

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
