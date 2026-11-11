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
    public BookData BookD { get; set; } = default!;
    public int BookID { get; set; }
    public int CategoryID { get; set; }

    public string TitleSort { get; set; } = string.Empty;
    public string AuthorSort { get; set; } = string.Empty;

    public string? CurrentFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public SelectList AuthorList { get; set; } = default!;

    public async Task OnGetAsync(int? id, int? categoryID, string? sortOrder, string? searchString)
    {
        BookD = new BookData();

        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";

        CurrentFilter = searchString;

        AuthorList = new SelectList(
            _context.Authors.Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName }),
            "ID", "FullName");

        IQueryable<Book> books = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking();

        if (AuthorID.HasValue)
        {
            books = books.Where(b => b.AuthorID == AuthorID);
        }

        BookD.Books = await books
            .OrderBy(b => b.Title)
            .ToListAsync();

        if (!String.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books.Where(s =>
                (s.Author != null && s.Author.FirstName.Contains(searchString))
                || (s.Author != null && s.Author.LastName.Contains(searchString))
                || s.Title.Contains(searchString));
        }

        if (id != null)
        {
            BookID = id.Value;
            Book? book = BookD.Books
                .SingleOrDefault(i => i.ID == id.Value);
            if (book != null && book.BookCategories != null)
            {
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
        }

        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Title);
                break;
            case "author_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Author?.FullName);
                break;
            case "author":
                BookD.Books = BookD.Books.OrderBy(s => s.Author?.FullName);
                break;
            default:
                BookD.Books = BookD.Books.OrderBy(s => s.Title);
                break;
        }
    }
}