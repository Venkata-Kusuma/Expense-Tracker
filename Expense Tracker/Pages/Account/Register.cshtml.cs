using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Expense_Tracker.Pages.Account
{
    public class RegisterModel : PageModel
    {
     
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public RegisterModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public Registerd Input { get; set; }

       
        public IActionResult OnGet()
        {
            return Page();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Input.Password != Input.ConfirmPassword)
            {
               
                return Page();
            }

            var user = new AppUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }

}
