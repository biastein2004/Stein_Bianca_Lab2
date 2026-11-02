using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Stein_Bianca_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Stein_Bianca_Lab2Context")
        ?? throw new InvalidOperationException("Connection string 'Stein_Bianca_Lab2Context' not found.")));

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();