using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyDinos.Models
{
    public class Balance
    {
        [Key]
        public int Id { get; set; } // Primary key

        [Required]
        public DateTime Date { get; set; } // The date for which the balance is recorded

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; } // The balance amount for that date

        [Required]
        public bool IsEstimated { get; set; } // Indicates if the balance is estimated
        
        
        // This static method uses the DbContext to get the most recent non-estimated balance
        public static Balance GetMostUpToDateBalance(ApplicationDbContext context)
        {
            // Query the Balances DbSet directly
            var mostRecentBalance = context.Balances
                .Where(b => !b.IsEstimated)
                .OrderByDescending(b => b.Date)
                .FirstOrDefault();

            // If no non-estimated balance exists, return a new balance with zero amount and current date
            if (mostRecentBalance == null)
            {
                return new Balance
                {
                    Date = DateTime.Now,
                    Amount = 0,
                    IsEstimated = false
                };
            }

            // Return the most recent non-estimated balance
            return mostRecentBalance;
        }
    }
}