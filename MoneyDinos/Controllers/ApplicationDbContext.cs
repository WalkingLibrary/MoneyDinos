using Microsoft.EntityFrameworkCore;
using MoneyDinos.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // For Pomelo
using Microsoft.EntityFrameworkCore;


public class ApplicationDbContext : DbContext
{
    public DbSet<Balance> Balances { get; set; } // Adding Balance to the DbContext
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Income> Incomes { get; set; } // Add this line for the Income model
    
    public DbSet<ExactEntry> ExactEntries { get; set; } // Add this line
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public Expense GetExpenseById(int id)
    {
        return Expenses.FirstOrDefault(e => e.Id == id);
    }

    public Income GetIncomeById(int id)
    {
        return Incomes.FirstOrDefault(i => i.Id == id);
    }
    
    public ExactEntry GetOverWritingExactEntry(int transactionId, DateTime dateToOverWrite)
    {
        
        return ExactEntries.FirstOrDefault(item => item.TransactionId == transactionId && item.Date.Date == dateToOverWrite.Date );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("name=DefaultConnection", new MySqlServerVersion(new Version(8, 0, 32)));
        }
    }
}