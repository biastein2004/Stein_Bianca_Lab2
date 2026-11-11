using Stein_Bianca_Lab2.Models;

namespace Stein_Bianca_Lab2.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Book>? Books { get; set; }
    }
}
