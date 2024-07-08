using FilmsRanking.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace FilmsRanking.Models
{
    public class MediaContent
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public float Rating { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? PosterImageUrl { get; set; }
        public MovieTypes Type { get; set; }
        public Genre Genre { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public string? ImagePublicId { get; set; }
    }
}
