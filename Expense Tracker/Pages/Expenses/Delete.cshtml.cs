using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Expense_Tracker.Pages.Expenses
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Expense = await _context.Expenses.FindAsync(id);
            if (Expense == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var expense = await _context.Expenses.FindAsync(Expense.Id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("ViewDetails");
        }
    }

}
