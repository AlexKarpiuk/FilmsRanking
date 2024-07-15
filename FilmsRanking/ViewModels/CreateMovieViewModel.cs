using FilmsRanking.Data.Enum;
using System.Net;

namespace FilmsRanking.ViewModels
{
    public class CreateMovieViewModel
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public float Rating { get; set; }
        public TimeSpan? Duration { get; set; }
        public IFormFile Image { get; set; }
        public MovieTypes Type { get; set; }
        public Genre Genre { get; set; }
    }

}
