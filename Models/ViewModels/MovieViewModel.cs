namespace MoviesApp.Models.ViewModels
{
    /// <summary>
    /// This is a view model of <see cref="Movie"/>
    /// </summary>
    public class MovieViewModel
    {
        [StringLength(70, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Director { get; set; }

        [Required]
        public Language Language { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [StringLength(250)]
        public string? Summary { get; set; }
    }
}
