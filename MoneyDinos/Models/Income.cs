using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyDinos.Models
{
    public class Income
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Specifies 18 digits in total, with 2 after the decimal point
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required]
        public IncomeCategory Category { get; set; } // Using enum for Category // e.g., Salary, Bonus, Investment, etc.

        public string Notes { get; set; } // Optional additional notes

        [Required]
        public bool IsRecurring { get; set; } // Indicates if the income is recurring

        public RecurrenceInterval? RecurrenceInterval { get; set; } // Nullable, since it only applies if IsRecurring is true

        [Required]
        public bool IsEstimated { get; set; } // Indicates if the income is estimated or an actual value
    }
}