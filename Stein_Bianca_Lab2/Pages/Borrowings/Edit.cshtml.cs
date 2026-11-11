using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stein_Bianca_Lab2.Models;

namespace Stein_Bianca_Lab2.Pages.BorrowingPages;

public class EditModel : PageModel
{
    private readonly Stein_Bianca_Lab2Context _context;

    public EditModel(Stein_Bianca_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    private void FillLists()
    {
        var members = _context.Member.OrderBy(m => m.LastName).ToList();
        ViewData["MemberID"] = new SelectList(
            members.Select(m => new { m.ID, FullName = m.FullName }),
            "ID", "FullName");

        var books = _context.Book.Include(b => b.Author).OrderBy(b => b.Title).ToList();
        ViewData["BookID"] = new SelectList(
            books.Select(b => new
            {
                b.ID,
                BookDetails = b.Title + " - " + (b.Author != null ? b.Author.FullName : "")
            }),
            "ID", "BookDetails");
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing == null)
        {
            return NotFound();
        }
        Borrowing = borrowing;
        FillLists();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            FillLists();
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Borrowing.Any(e => e.ID == Borrowing.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }
}