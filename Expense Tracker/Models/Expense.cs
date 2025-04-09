using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Comments { get; set; }

         public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ValidateNever]
        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        [ValidateNever]
        public AppUser AppUser { get; set; }
    }
}
