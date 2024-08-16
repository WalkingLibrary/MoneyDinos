using Microsoft.AspNetCore.Mvc;
using MoneyDinos.Models;

namespace MoneyDinos.Controllers;

public class ReportController : Controller
{
    
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public ReportController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    //Reports

    public IActionResult ReportsList()
    {
        return View();
    }
    
    public IActionResult ProjectedBalanceByDays()
    {
        var model = new ReportViewModel
        {
            Balances = _context.Balances.ToList(),
            Incomes = _context.Incomes.ToList(),
            Expenses = _context.Expenses.ToList()
        };

        return View(model);
    }
    
    // GET: Display the form to input the date range
    public IActionResult ProjectedBalanceReport()
    {
        return View(new ProjectedBalanceViewModel());
    }

    // POST: Calculate and display the projected balances
    [HttpPost]
    public IActionResult ProjectedBalanceReport(ProjectedBalanceViewModel model)
    {
        model.ProjectedBalances = new List<Balance>();

        for (DateTime date = model.StartDate; date <= model.EndDate; date = date.AddDays(1))
        {
            decimal incomeSum = _context.Incomes
                .Where(i => i.Date <= date && (!i.IsRecurring || (i.IsRecurring && i.RecurrenceInterval == RecurrenceInterval.Daily)))
                .Sum(i => i.Amount);

            decimal expenseSum = _context.Expenses
                .Where(e => e.Date <= date && (!e.IsRecurring || (e.IsRecurring && e.RecurrenceInterval == RecurrenceInterval.Daily)))
                .Sum(e => e.Amount);

            decimal balanceAmount = incomeSum - expenseSum;

            model.ProjectedBalances.Add(new Balance
            {
                Date = date,
                Amount = balanceAmount,
                IsEstimated = true
            });
        }

        return View(model);
    }

}