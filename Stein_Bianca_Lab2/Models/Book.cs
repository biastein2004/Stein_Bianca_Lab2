using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stein_Bianca_Lab2.Models
{
    public class Book
    {

        public int ID { get; set; }

        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "Titlul cartii este obligatoriu")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Titlul trebuie sa aiba intre 3 si 150 de caractere")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 500)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        public int? AuthorID { get; set; }
        public Authors? Author { get; set; }
        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }
        public ICollection<Borrowing>? Borrowings { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}