using System.ComponentModel.DataAnnotations;

namespace mybookish.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
