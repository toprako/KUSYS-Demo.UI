using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.ViewModels.Account
{
    public class SignInViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "30 Karakteri Geçtiniz")]
        [Display(Name ="E-Mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "30 Karakteri Geçtiniz")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
