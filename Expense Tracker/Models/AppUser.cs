using Microsoft.AspNetCore.Identity;

namespace Expense_Tracker.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Expense> Expenses { get; set; }
    }

}
