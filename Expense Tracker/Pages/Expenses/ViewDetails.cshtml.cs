using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Pages.Expenses
{
    public class ViewDetailsModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ViewDetailsModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Expense> Expenses { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge(); 
            }

            Expenses = await _context.Expenses
                .Where(e => e.AppUserId == user.Id)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

            return Page();
        }

    }
}
