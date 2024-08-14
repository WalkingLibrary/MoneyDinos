using Microsoft.EntityFrameworkCore;
using MoneyDinos.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // For Pomelo
using Microsoft.EntityFrameworkCore;


public class ApplicationDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

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