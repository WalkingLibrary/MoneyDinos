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
    }
}