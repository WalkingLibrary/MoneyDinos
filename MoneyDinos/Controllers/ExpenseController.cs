using Microsoft.AspNetCore.Mvc;
using MoneyDinos.Models;

namespace MoneyDinos.Controllers;

public class ExpenseController: Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public ExpenseController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
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
            return RedirectToAction("ExpenseList"); // Redirect to a list or summary page after saving
        }
    
        return View(model); // If validation fails, return to the form with validation messages
    }


    
    public IActionResult ExpenseList()
    {
        var expenses = _context.Expenses.ToList();
        return View(expenses);
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

}