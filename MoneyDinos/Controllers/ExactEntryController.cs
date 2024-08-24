using Microsoft.AspNetCore.Mvc;
using MoneyDinos.Models;
using System.Linq;

public class ExactEntryController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExactEntryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult ExactEntryList()
    {
        var exactEntries = _context.ExactEntries.ToList();
        return View(exactEntries);
    }
    
    public IActionResult ExactEntryEdit(int id)
    {
        var exactEntry = _context.ExactEntries.Find(id);
        if (exactEntry == null)
        {
            return NotFound();
        }
        return View(exactEntry);
    }

    [HttpPost]
    public IActionResult ExactEntryEdit(ExactEntry exactEntry)
    {
        ModelState.Remove("TransactionDetails"); // This removes any ModelState errors related to TransactionDetails
        if (ModelState.IsValid)
        {
            _context.ExactEntries.Update(exactEntry);
            _context.SaveChanges();
            return RedirectToAction(nameof(ExactEntryList));
        }
        return View(exactEntry);
    }

    
    public IActionResult ExactEntryCreate(int transactionId, bool isExpense, DateTime date, decimal amount)
    {
        
        object transactionDetails = isExpense ?
            _context.GetExpenseById(transactionId):
            _context.GetIncomeById(transactionId);
        if (transactionDetails == null)
        {
            // Handle error, such as logging and returning an appropriate view or message
            return NotFound($"Transaction with ID {transactionId} not found.");
        }

        var model = new ExactEntry
        {
            TransactionId = transactionId,
            IsExpense = isExpense,
            Date = Convert.ToDateTime(date.Date.ToString("yyyy-MM-dd")), // Formats the date to YYYY-MM-DD
            Amount = amount,
            TransactionDetails = transactionDetails // This will be either Income or Expense model
        };
        
        return View(model);
    }

    [HttpPost]
    public IActionResult ExactEntryCreate(ExactEntry exactEntry)
    {
        ModelState.Remove("TransactionDetails"); // This removes any ModelState errors related to TransactionDetails
        if (ModelState.IsValid)
        {
            _context.ExactEntries.Add(exactEntry);
            _context.SaveChanges();
            return RedirectToAction(nameof(ExactEntryList));
        }
        return View(exactEntry);
    }

    
    [HttpPost]
    public IActionResult DeleteExactEntry(int id)
    {
        var entry = _context.ExactEntries.Find(id);
        if (entry != null)
        {
            _context.ExactEntries.Remove(entry);
            _context.SaveChanges();
            return RedirectToAction(nameof(ExactEntryList));
        }
        return NotFound();
    }

}