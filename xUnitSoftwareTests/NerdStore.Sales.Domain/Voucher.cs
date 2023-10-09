using FluentValidation;

namespace NerdStore.Sales.Domain
{
    public class Voucher
    {
        public Voucher(string code, decimal? discountPercentage, decimal? discountValue,int quantity, ETypeOfDiscountVoucher typeOfDiscountVoucher, DateTime expirationDate, bool active, bool used)
        {
            Code = code;
            DiscountPercentage = discountPercentage;
            DiscountValue = discountValue;
            Quantity = quantity;
            TypeOfDiscountVoucher = typeOfDiscountVoucher;
            ExpirationDate = expirationDate;
            Active = active;
            Used = used;
        }

        public string Code { get; private set; }
        public decimal? DiscountPercentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public ETypeOfDiscountVoucher TypeOfDiscountVoucher { get; private set; }
        public DateTime ExpirationDate { get; private set;}
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        public bool ValidateIfIsApplicable()
        {
            return true;
        }

        public class VoucherApplicableValidation : AbstractValidator<Voucher>
        {
            public static string VoucherWihoutValidCode => "Voucher without valid code.";
            public static string ExpiredVoucher => "This voucher is expired.";
            public static string NoLongerValidVoucher => "This voucher is no longer valid.";
            public static string VoucherAlreadyUsed => "This voucher has already been used.";
            public static string NoLongerAvaibleVoucher => "This voucher is no longer available";
            public static string ValueGreaterThanZero => "The discount value must be greater than 0";
            public static string PercentageValueGreaterThanZero => "The discount percentage value must be greater than 0";
        }

    }
}
