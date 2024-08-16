using Microsoft.AspNetCore.Mvc;
using MoneyDinos.Models;
using MoneyDinos.Util;

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

        Balance mostRecentBalance = Balance.GetMostUpToDateBalance(_context);
        decimal currentBalance = mostRecentBalance.Amount;
        for (DateTime date = model.StartDate; date <= model.EndDate; date = date.AddDays(1))
        {
            decimal intervalsTotalIncome = ReportUtil.GetIncomeTotal(date, date, _context.Incomes.ToList());
            decimal intervalsTotalExpense = ReportUtil.GetExpenseTotal(date, date, _context.Expenses.ToList());

            currentBalance = currentBalance + intervalsTotalIncome;
            currentBalance = currentBalance - intervalsTotalExpense;
            model.ProjectedBalances.Add(new Balance
            {
                Date = date,
                Amount = currentBalance,
                IsEstimated = true
            });
        }

        return View(model);
    }

}