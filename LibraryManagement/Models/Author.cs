using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Author
    {
         public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50,MinimumLength =3, ErrorMessage = "Full Name cannot exceed 50 characters")]
        public string AuthorName { get; set; }

    }
}
