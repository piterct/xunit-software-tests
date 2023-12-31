﻿using Xunit;
using static NerdStore.Sales.Domain.Voucher;

namespace NerdStore.Sales.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validate a type value valid voucher ")]
        [Trait("Category", "Sales - Voucher")]
        public void Voucher_ValidateATypeValidValueVoucher_MustBeValid()
        {

            // Arrange
            var voucher = new Voucher("OFF-15", null, 15, 1, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(15),
                true, false);

            // Act
            var result = voucher.ValidateIfIsApplicable();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validate a type invalid voucher")]
        [Trait("Category", "Sales - Voucher")]
        public void Voucher_ValidateATypeInvalidVoucher_MustBeInvalid()
        {
            // Arrange
            var voucher = new Voucher("", null, null, 0, ETypeOfDiscountVoucher.Value, DateTime.Now.AddDays(-1),
                false, true);

            // Act
            var result = voucher.ValidateIfIsApplicable();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherApplicableValidation.VoucherWithoutValidCode, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.ExpiredVoucher, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.NoLongerValidVoucher, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.VoucherAlreadyUsed, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.NoLongerAvaibleVoucher, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.ValueGreaterThanZero, result.Errors.Select(c => c.ErrorMessage));
        }


        [Fact(DisplayName = "Validate percentage voucher valid")]
        [Trait("Category", "Sales - Voucher")]
        public void Voucher_ValidateValidPercentageVoucher_MustBeValid()
        {
            // Arrange
            var voucher = new Voucher("OFF-15", 15, null, 1, ETypeOfDiscountVoucher.Percentage, DateTime.Now.AddDays(15),
                true, false);

            // Act
            var result = voucher.ValidateIfIsApplicable();

            // Assert
            Assert.True(result.IsValid);
        }


        [Fact(DisplayName = "Validate percentage voucher invalid")]
        [Trait("Category", "Sales - Voucher")]
        public void Voucher_ValidateInValidPercentageVoucher_MustBeInValid()
        {
            // Arrange
            var voucher = new Voucher("", null, null, 0, ETypeOfDiscountVoucher.Percentage, DateTime.Now.AddDays(-1), false, true);

            // Act
            var result = voucher.ValidateIfIsApplicable();

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(VoucherApplicableValidation.VoucherWithoutValidCode, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.ExpiredVoucher, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.NoLongerValidVoucher, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.VoucherAlreadyUsed, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.NoLongerAvaibleVoucher, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherApplicableValidation.PercentageValueGreaterThanZero, result.Errors.Select(c => c.ErrorMessage));
        }
    }
}
