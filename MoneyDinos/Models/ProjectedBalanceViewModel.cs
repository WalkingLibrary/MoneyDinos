using System;
using System.Collections.Generic;

namespace MoneyDinos.Models
{
    public class ProjectedBalanceViewModel
    {
        public DateTime StartDate { get; set; } // User-input start date
        public DateTime EndDate { get; set; } // User-input end date
        public List<Balance> ProjectedBalances { get; set; } // Calculated projected balances
    }
}