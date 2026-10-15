using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stein_Bianca_Lab2.Models;

namespace Stein_Bianca_Lab2.Pages.Books;

public class CreateModel : PageModel
{
    private readonly Stein_Bianca_Lab2Context _context;

    public CreateModel(Stein_Bianca_Lab2Context context)
    {
        _context = context;
    }

    private void FillLists()
    {
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
        ViewData["AuthorID"] = new SelectList(
            _context.Authors.Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName }),
            "ID", "FullName");
    }

    public IActionResult OnGet()
    {
        FillLists();
        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            FillLists();
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}