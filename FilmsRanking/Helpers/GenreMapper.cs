using FilmsRanking.Data.Enum;
using FilmsRanking.ViewModels;

namespace FilmsRanking.Helpers
{
    public class GenreMapper
    {
        public static readonly Dictionary<int, Genre> GenreMap = new Dictionary<int, Genre>
        {
            { 28, Genre.Action },
            { 35, Genre.Comedy },
            { 18, Genre.Drama },
            { 27, Genre.Horror },
            { 878, Genre.ScienceFiction },
            { 12, Genre.Adventure },
            { 53, Genre.Thriller },
            { 10749, Genre.Romance },
            { 80, Genre.Crime },
            { 14, Genre.Fantasy },
            { 99, Genre.Documentary },
            { 37, Genre.Western },
            { 10752, Genre.War },
            { 36, Genre.Historical },
            { 10402, Genre.Music },
            { 16, Genre.Animation },
            { 10751, Genre.Family },
            { 10770, Genre.TVMovie },
        };

        public static Genre MapGenres(List<GenreApi> genres)
        {
            if (genres == null || genres.Count == 0)
            {
                return 0;
            }

            var firstGenre = genres.FirstOrDefault();
            if (firstGenre != null && GenreMapper.GenreMap.ContainsKey(firstGenre.id))
            {
                return GenreMapper.GenreMap[firstGenre.id];
            }

            return 0;
        }
    }
}
