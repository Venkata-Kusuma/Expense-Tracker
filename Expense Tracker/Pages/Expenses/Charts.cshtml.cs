using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace Expense_Tracker.Pages.Expenses
{
    public class ChartsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ChartsModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public string CategoryDataJson { get; set; }

     

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge(); 
            }

            var data = await _context.Expenses
                .Where(e => e.AppUserId == user.Id && !string.IsNullOrEmpty(e.Category))
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(x => x.Amount)
                })
                .ToListAsync();

            CategoryDataJson = JsonSerializer.Serialize(data);

            return Page();
        }

    }
}