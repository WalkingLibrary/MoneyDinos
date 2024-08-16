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


    
   
    //Balences
    // List Balances
    public IActionResult BalanceList()
    {
        var balances = _context.Balances.ToList();
        return View(balances);
    }

    // Create Balance - GET
    public IActionResult CreateBalance()
    {
        return View();
    }

    // Create Balance - POST
    [HttpPost]
    public IActionResult CreateBalance(Balance model)
    {
        if (ModelState.IsValid)
        {
            _context.Balances.Add(model);
            _context.SaveChanges();
            return RedirectToAction("BalanceList");
        }
        return View(model);
    }

    // Edit Balance - GET
    public IActionResult EditBalance(int id)
    {
        var balance = _context.Balances.Find(id);
        if (balance == null)
        {
            return NotFound();
        }
        return View(balance);
    }

    // Edit Balance - POST
    [HttpPost]
    public IActionResult EditBalance(Balance model)
    {
        if (ModelState.IsValid)
        {
            _context.Balances.Update(model);
            _context.SaveChanges();
            return RedirectToAction("BalanceList");
        }
        return View(model);
    }

    // Delete Balance - POST
    [HttpPost]
    public IActionResult DeleteBalance(int id)
    {
        var balance = _context.Balances.Find(id);
        if (balance != null)
        {
            _context.Balances.Remove(balance);
            _context.SaveChanges();
        }
        return RedirectToAction("BalanceList");
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
