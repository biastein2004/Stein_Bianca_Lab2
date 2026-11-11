
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Stein_Bianca_Lab2.Data;

public class Stein_Bianca_Lab2IdentityContext
    : IdentityDbContext<IdentityUser>
{
    public Stein_Bianca_Lab2IdentityContext(
        DbContextOptions<Stein_Bianca_Lab2IdentityContext> options)
        : base(options)
    {
    }
}
