using Microsoft.AspNetCore.Mvc;
using MoneyDinos.Models;

namespace MoneyDinos.Controllers;

public class BalanceController: Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public BalanceController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
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
}