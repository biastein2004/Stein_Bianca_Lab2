using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stein_Bianca_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList = new List<AssignedCategoryData>();

        public void PopulateAssignedCategoryData(Stein_Bianca_Lab2Context context,
                                                 Book book)
        {
            var allCategories = context.Category;
            var bookCategories = new HashSet<int>(
                (book.BookCategories ?? new List<BookCategory>()).Select(c => c.CategoryID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.ID)
                });
            }
        }

        public void UpdateBookCategories(Stein_Bianca_Lab2Context context,
            string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            if (bookToUpdate.BookCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>
                (bookToUpdate.BookCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!bookCategories.Contains(cat.ID))
                    {
                        bookToUpdate.BookCategories.Add(
                            new BookCategory
                            {
                                BookID = bookToUpdate.ID,
                                CategoryID = cat.ID
                            });
                    }
                }
                else
                {
                    if (bookCategories.Contains(cat.ID))
                    {
                        BookCategory? bookToRemove
                            = bookToUpdate
                                .BookCategories
                                .SingleOrDefault(i => i.CategoryID == cat.ID);
                        if (bookToRemove != null)
                        {
                            context.Remove(bookToRemove);
                        }
                    }
                }
            }
        }
    }
}
