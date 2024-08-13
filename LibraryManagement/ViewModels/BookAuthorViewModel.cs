using LibraryManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.ViewModels
{
    [NotMapped]
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100)]
        [MinLength(2)]
        public string Title { get; set; }

        [StringLength(150, ErrorMessage = "Description cannot exceed 150 characters")]
        public string Description { get; set; }

        [RegularExpression("^[0-9]{13}$", ErrorMessage = "ISBN must be a 13-digit number")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Publication Date is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date")]
        
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }
        public int AuthorId { get; set; }
        public List<Author>  Authors{ get; set; }
    }
}
