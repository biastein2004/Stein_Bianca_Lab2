using Microsoft.EntityFrameworkCore;

public class Stein_Bianca_Lab2Context(DbContextOptions<Stein_Bianca_Lab2Context> options) : DbContext(options)
{
    public DbSet<Stein_Bianca_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Stein_Bianca_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Stein_Bianca_Lab2.Models.Authors> Authors { get; set; } = default!;
    public DbSet<Stein_Bianca_Lab2.Models.Category> Category { get; set; } = default!;
    public DbSet<Stein_Bianca_Lab2.Models.BookCategory> BookCategory { get; set; } = default!;
}
