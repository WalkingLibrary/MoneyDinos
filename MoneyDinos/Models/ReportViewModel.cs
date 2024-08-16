using System.Collections.Generic;

namespace MoneyDinos.Models
{
    public class ReportViewModel
    {
        public IEnumerable<Balance> Balances { get; set; }
        public IEnumerable<Income> Incomes { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
    }
}