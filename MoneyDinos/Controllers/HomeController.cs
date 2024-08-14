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
