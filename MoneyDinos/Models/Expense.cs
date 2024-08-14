using System;
using System.ComponentModel.DataAnnotations;
namespace MoneyDinos.Models;

public class Expense
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
    public decimal Amount { get; set; }

    [Required]
    public string Category { get; set; } // e.g., Food, Utilities, Rent, etc.

    public string Notes { get; set; } // Optional additional notes
    
    // New fields for recurring expenses
    [Required]
    public bool IsRecurring { get; set; } // Indicates if the expense is recurring

    public RecurrenceInterval? RecurrenceInterval { get; set; } // Nullable, since it only applies if IsRecurring is true
}

// Enum to represent different recurrence intervals
public enum RecurrenceInterval
{
    Daily,
    Weekly,
    BiWeekly,
    Monthly,
    Yearly
}