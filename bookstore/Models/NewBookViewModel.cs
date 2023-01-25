using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    public class NewBookViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Book title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "ImageURL")]
        public string ImageURL { get; set; }

        [Display(Name = "Price in RSD")]
        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

        [Display(Name = "Year of publication")]
        [Required(ErrorMessage = "Year is required")]
        public int YearOfPublication { get; set; }

        [Display(Name = "Select author")]
        [Required(ErrorMessage = "Book author is required")]
        public int AuthorId { get; set; }

        [Display(Name = "Select a book genre")]
        [Required(ErrorMessage = "Book Genre is required")]
        public int BookGenreId { get; set; }

    }
}
