using FilmsRanking.Data.Enum;

namespace FilmsRanking.ViewModels
{
    public class EditMovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public float Rating { get; set; }
        public IFormFile? Image { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? PosterImageUrl { get; set; }
        public MovieTypes Type { get; set; }
    }
}
