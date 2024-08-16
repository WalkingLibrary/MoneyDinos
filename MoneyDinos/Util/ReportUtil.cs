using MoneyDinos.Models;

namespace MoneyDinos.Util;

public class ReportUtil
{
    public static decimal GetExpenseTotal(DateTime startDate, DateTime endDate, List<Expense> expenses)
    {
        decimal total = 0;

        foreach (var expense in expenses)
        {
            if (!expense.IsRecurring)
            {
                // If the expense is not recurring, check if it falls within the date range
                if (expense.Date >= startDate && expense.Date <= endDate)
                {
                    total += (decimal)expense.Amount;
                }
            }
            else
            {
                // Handle recurring expenses
                DateTime currentDate = expense.Date;

                while (currentDate <= endDate)
                {
                    if (currentDate >= startDate && currentDate <= endDate)
                    {
                        total += (decimal)expense.Amount;
                    }

                    // Determine the next occurrence date based on the recurrence interval
                    currentDate = GetNextOccurrenceDate(currentDate, expense.RecurrenceInterval);
                }
            }
        }

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

    
    public static decimal GetIncomeTotal(DateTime startDate, DateTime endDate, List<Income> incomes)
    {
        decimal total = 0;

        foreach (var income in incomes)
        {
            if (!income.IsRecurring)
            {
                // If the income is not recurring, check if it falls within the date range
                if (income.Date >= startDate && income.Date <= endDate)
                {
                    total += (decimal)income.Amount;
                }
            }
            else
            {
                // Handle recurring incomes
                DateTime currentDate = income.Date;

                while (currentDate <= endDate)
                {
                    if (currentDate >= startDate && currentDate <= endDate)
                    {
                        total += (decimal)income.Amount;
                    }

                    // Determine the next occurrence date based on the recurrence interval
                    currentDate = GetNextOccurrenceDate(currentDate, income.RecurrenceInterval);
                }
            }
        }

        return total;
    }


   
}