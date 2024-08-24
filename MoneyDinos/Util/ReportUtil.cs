using MoneyDinos.Models;

namespace MoneyDinos.Util;

public class ReportUtil
{
    public static decimal GetExpenseTotal(ApplicationDbContext context,DateTime startDate, DateTime endDate, List<Expense> expenses, Balance balance)
    {
        decimal total = 0;
        List<Expense> expenseUsedToCalculate = new List<Expense>();
        Expense expenseToUse;
        foreach (var expense in expenses)
        {
            expenseToUse = expense;
            if (!expenseToUse.IsRecurring)
            {
                ExactEntry exactEntry = context.GetOverWritingExactEntry(expense.Id, expense.Date);
                if (exactEntry != null)
                {
                    expenseToUse = new Expense()
                    {
                        Amount = exactEntry.Amount,
                        Date = expense.Date,
                        IsRecurring = expense.IsRecurring,
                    };
                }
                // If the expense is not recurring, check if it falls within the date range
                if (expenseToUse.Date >= startDate && expenseToUse.Date <= endDate)
                {
                    expenseUsedToCalculate.Add(expenseToUse);
                    total += (decimal)expenseToUse.Amount;
                }
            }
            else
            {
                // Handle recurring expenses
                DateTime currentDate = expenseToUse.Date;
                

                while (currentDate <= endDate)
                {
                    expenseToUse = expense;
                    ExactEntry exactEntry = context.GetOverWritingExactEntry(expense.Id, currentDate);
                    if (exactEntry != null)
                    {
                        expenseToUse = new Expense()
                        {
                            Amount = exactEntry.Amount,
                            Date = expense.Date,
                            IsRecurring = expense.IsRecurring,
                        };
                    }
                    if (currentDate >= startDate && currentDate <= endDate)
                    {
                        
                        expenseUsedToCalculate.Add(expenseToUse);
                        total += (decimal)expenseToUse.Amount;
                    }

                    // Determine the next occurrence date based on the recurrence interval
                    currentDate = GetNextOccurrenceDate(currentDate, expense.RecurrenceInterval);
                }
            }
        }

        balance.ExpensesUsedToCalculate = expenseUsedToCalculate;
        return total;
    }

    private static DateTime GetNextOccurrenceDate(DateTime currentDate, RecurrenceInterval? interval)
    {
        switch (interval)
        {
            case RecurrenceInterval.Daily:
                return currentDate.AddDays(1);

            case RecurrenceInterval.Weekly:
                return currentDate.AddDays(7);

            case RecurrenceInterval.BiWeekly:
                return currentDate.AddDays(14);

            case RecurrenceInterval.Monthly:
                return currentDate.AddMonths(1);

            case RecurrenceInterval.BiMonthly:
                return currentDate.AddMonths(2);

            case RecurrenceInterval.TriMonthly:
                return currentDate.AddMonths(3);

            case RecurrenceInterval.SixMonths:
                return currentDate.AddMonths(6);

            case RecurrenceInterval.Yearly:
                return currentDate.AddYears(1);

            default:
                throw new ArgumentOutOfRangeException(nameof(interval), "Unsupported recurrence interval");
        }
    }

    
    public static decimal GetIncomeTotal(ApplicationDbContext context, DateTime startDate, DateTime endDate, List<Income> incomes, Balance balance)
    {
        decimal total = 0;
        List<Income> incomesUsedToCalculate = new List<Income>();
        Income incomeToUse;
        foreach (var income in incomes)
        {
            incomeToUse = income;
            if (!incomeToUse.IsRecurring)
            {
                ExactEntry exactEntry = context.GetOverWritingExactEntry(income.Id, income.Date);
                if (exactEntry != null)
                {
                    incomeToUse = new Income()
                    {
                        Amount = exactEntry.Amount,
                        Date = income.Date,
                        IsRecurring = income.IsRecurring,
                    };
                }
                // If the income is not recurring, check if it falls within the date range
                if (incomeToUse.Date >= startDate && incomeToUse.Date <= endDate)
                {
                    incomesUsedToCalculate.Add(incomeToUse);
                    total += (decimal)incomeToUse.Amount;
                }
            }
            else
            {
                // Handle recurring incomes
                DateTime currentDate = incomeToUse.Date;

                while (currentDate <= endDate)
                {
                    incomeToUse = income;
                    ExactEntry exactEntry = context.GetOverWritingExactEntry(income.Id, currentDate);
                    if (exactEntry != null)
                    {
                        incomeToUse = new Income()
                        {
                            Amount = exactEntry.Amount,
                            Date = income.Date,
                            IsRecurring = income.IsRecurring,
                        };
                    }
                    if (currentDate >= startDate && currentDate <= endDate)
                    {
                        incomesUsedToCalculate.Add(incomeToUse);
                        total += (decimal)incomeToUse.Amount;
                    }

                    // Determine the next occurrence date based on the recurrence interval
                    currentDate = GetNextOccurrenceDate(currentDate, income.RecurrenceInterval);
                }
            }
        }

        balance.IncomesUsedToCalculate = incomesUsedToCalculate;
        return total;
    }


   
}