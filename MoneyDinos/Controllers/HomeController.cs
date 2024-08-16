using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyDinos.Models;

namespace MoneyDinos.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        TestDbConnection();
        return View();
    }

    public IActionResult CreateExpense()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateExpense(Expense model)
    {
        if (ModelState.IsValid)
        {
            _context.Expenses.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index"); // Redirect to a list or summary page after saving
        }
    
        return View(model); // If validation fails, return to the form with validation messages
    }


    
    public IActionResult ExpenseList()
    {
        var expenses = _context.Expenses.ToList();
        return View(expenses);
    }

    
    
    public IActionResult EditExpense(int id)
    {
        var expense = _context.Expenses.Find(id);
        if (expense == null)
        {
            return NotFound();
        }
        return View(expense);
    }

    
    [HttpPost]
    public IActionResult EditExpense(Expense model)
    {
        if (ModelState.IsValid)
        {
            _context.Expenses.Update(model);
            _context.SaveChanges();
            return RedirectToAction("ExpenseList"); // Redirect to the expense list after saving
        }

        return View(model); // If validation fails, return to the form with validation messages
    }

    
    //INcomes
    public IActionResult IncomeList()
    {
        var incomes = _context.Incomes.ToList();
        return View(incomes);
    }

    public IActionResult CreateIncome()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateIncome(Income model)
    {
        if (ModelState.IsValid)
        {
            _context.Incomes.Add(model);
            _context.SaveChanges();
            return RedirectToAction("IncomeList");
        }

        return View(model); // Return the view with validation messages if the model is invalid
    }

    
    public IActionResult EditIncome(int id)
    {
        var income = _context.Incomes.Find(id);
        if (income == null)
        {
            return NotFound();
        }
        return View(income);
    }

    [HttpPost]
    public IActionResult EditIncome(Income model)
    {
        if (ModelState.IsValid)
        {
            _context.Incomes.Update(model);
            _context.SaveChanges();
            return RedirectToAction("IncomeList");
        }

        return View(model); // Return the view with validation messages if the model is invalid
    }
    
// Delete action for Income
    [HttpPost]
    public IActionResult DeleteIncome(int id)
    {
        var income = _context.Incomes.Find(id);
        if (income != null)
        {
            _context.Incomes.Remove(income);
            _context.SaveChanges();
        }
        return RedirectToAction("IncomeList");
    }

// Delete action for Expense
    [HttpPost]
    public IActionResult DeleteExpense(int id)
    {
        var expense = _context.Expenses.Find(id);
        if (expense != null)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }
        return RedirectToAction("ExpenseList");
    }

    
    //Reports
    public IActionResult MonthlyIncomeReport()
    {
        var incomes = _context.Incomes.ToList();
        var expenses = _context.Expenses.ToList();
        return View(incomes);
    }

    public IActionResult ReportsList()
    {
        return View();
    }
    
    // Example action to test the database connection
    public IActionResult TestDbConnection()
    {
        try
        {
            var count = _context.Expenses.Count(); // Or any simple query
            return Ok($"Database connected successfully. Total expenses: {count}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Database connection failed: {ex.Message}");
        }
    }
}
