using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Expense_Tracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel(ILogger<IndexModel> logger, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        [BindProperty]
        public Logind Input { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Expenses/ViewDetails");
            }
            return Page();
        }
    }
}
