using Microsoft.AspNetCore.Mvc;
using MoneyDinos.Models;

namespace MoneyDinos.Controllers;

public class IncomeController: Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public IncomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
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
}