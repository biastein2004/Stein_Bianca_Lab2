using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stein_Bianca_Lab2.Models;

namespace Stein_Bianca_Lab2.Pages.Books;

public class IndexModel : PageModel
{
    private readonly Stein_Bianca_Lab2Context _context;

    public IndexModel(Stein_Bianca_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public SelectList AuthorList { get; set; } = default!;

    public async Task OnGetAsync()
    {
        AuthorList = new SelectList(
            _context.Authors.Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName }),
            "ID", "FullName");

        var books = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .AsQueryable();

        if (AuthorID.HasValue)
        {
            books = books.Where(b => b.AuthorID == AuthorID);
        }

        Book = await books.ToListAsync();
    }
}