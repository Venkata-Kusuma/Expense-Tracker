using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Expense_Tracker.Pages.Expenses
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Expense = await _context.Expenses.FindAsync(id);
            if (Expense == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var existing = await _context.Expenses.FindAsync(Expense.Id);
            if (existing == null) return NotFound();

            existing.Category = Expense.Category;
            existing.Amount = Expense.Amount;
            existing.Comments = Expense.Comments;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return RedirectToPage("ViewDetails");
        }
    }

}
