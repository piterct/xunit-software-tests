using Xunit;

namespace NerdStore.Sales.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validate a type value valid voucher ")]
        [Trait("Category", "Sales - Voucher")]
        public void Voucher_ValidateATypeValueVoucherValida_MustBeValid()
        {
            // Arrange
            var order = Order.OrderFactory.NewOrderDraft(Guid.NewGuid());



           
        }
    }
}
