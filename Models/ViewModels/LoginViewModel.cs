namespace MoviesApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
