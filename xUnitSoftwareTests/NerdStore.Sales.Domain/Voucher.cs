using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Domain
{
    public class Voucher
    {
        //public Voucher(string code, decimal discountValue, decimal discountPercentage, DateTime? expirationDate, bool active, bool used)
        //{
        //    Code = code;
        //    DiscountValue = discountValue;
        //    DiscountPercentage = discountPercentage;
        //    ExpirationDate = expirationDate;
        //    Active = active;
        //    Used = used;
        //}

        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime? ExpirationDate { get; set;}
        public bool Active { get; set; }
        public bool Used { get; set; }

    }
}
