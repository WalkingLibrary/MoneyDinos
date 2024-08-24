using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyDinos.Models
{
    public class ExactEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; } // The date on which the override applies

        [Required]
        public bool IsExpense { get; set; } // Indicates whether this is overriding an Expense (true) or an Income (false)

        [Required]
        public int TransactionId { get; set; } // References the overridden Expense or Income
        
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Amount Cannot Be Negative")]
        public decimal Amount { get; set; } // The exact amount to override with

        [StringLength(200)]
        public string Reason { get; set; } // Optional: Reason for the override
        
        // Transient property, not saved to the database
        [NotMapped] // This tells EF Core to ignore this property
        public dynamic TransactionDetails { get; set; }
    }
}
