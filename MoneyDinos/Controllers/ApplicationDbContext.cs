using Microsoft.EntityFrameworkCore;
using MoneyDinos.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // For Pomelo
using Microsoft.EntityFrameworkCore;


public class ApplicationDbContext : DbContext
{
    public DbSet<Balance> Balances { get; set; } // Adding Balance to the DbContext
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Income> Incomes { get; set; } // Add this line for the Income model

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("name=DefaultConnection", new MySqlServerVersion(new Version(8, 0, 32)));
        }
    }
}