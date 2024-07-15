namespace FilmsRanking.ViewModels
{
    public class ApiMovieViewModel
    {

    }

    public class Root
    {
        public List<MovieLink> results { get; set; }
    }

    public class MovieLink
    {
        public int id { get; set; }      
    }

    public class GenreApi
    { 
        public int id { get; set; }
        public string? name { get; set; }
    }

    public class MovieToDb
    {
        public int id { get; set; }
        public List<GenreApi> genres { get; set; }
        public string? poster_path { get; set; }
        public string tagline { get; set; }
        public int runtime { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public float vote_average { get; set; }

    }
}
