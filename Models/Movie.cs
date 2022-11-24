namespace MoviesApp.Models
{
    public enum Language
    {
        English,
        Malayalam
    }

    public class Movie
    {
        public int Id { get; set; }

        [StringLength(70)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        public Language Language { get; set; }

        public DateTime ReleaseDate { get; set; }

        [StringLength(250)]
        public string? Summary { get; set; }
    }
}
