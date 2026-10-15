using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stein_Bianca_Lab2.Models;

namespace Stein_Bianca_Lab2.Pages.Books;

public class DetailsModel : PageModel
{
    private readonly Stein_Bianca_Lab2Context _context;

    public DetailsModel(Stein_Bianca_Lab2Context context)
    {
        _context = context;
    }

    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        return Page();
    }
}