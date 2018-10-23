using System;

namespace VideotapeGalore.Services.Filters
{
    public class BorrowFilter
    {
        public DateTime? LoanDate { get; set; }
        public int? LoanDuration { get; set; }
    }
}