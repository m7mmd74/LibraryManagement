using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        public String UserName {get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        public String Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Required]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }

        
    }
}
