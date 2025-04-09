using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Expense_Tracker.Pages.Expenses
{
    [Authorize]
    public class AddModel : PageModel
    {
     
            private readonly ApplicationDbContext _context;
            private readonly UserManager<AppUser> _userManager;

            public AddModel(ApplicationDbContext context, UserManager<AppUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            [BindProperty]
            public Expense Expense { get; set; }

            public IActionResult OnGet()
            {
                return Page();
            }

     
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge(); 
            }
            Expense.AppUserId = user.Id;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Expenses.Add(Expense);
            await _context.SaveChangesAsync();

            return RedirectToPage("ViewDetails");
        }

    }

}

