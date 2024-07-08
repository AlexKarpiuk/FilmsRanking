using FilmsRanking.Data;
using FilmsRanking.Data.Enum;
using FilmsRanking.Helpers;
using FilmsRanking.Interfaces;
using FilmsRanking.Models;
using FilmsRanking.ViewModels;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace FilmsRanking.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MovieController : Controller
    {
        private readonly IMediaContentRepository _mediaContentRepository;
        private readonly IPhotoService _photoService;
        private readonly ISanitizerService _sanitizerService;

        public MovieController(IMediaContentRepository mediaContentRepository, IPhotoService photoService, ISanitizerService sanitizerService)
        {
            _mediaContentRepository = mediaContentRepository;
            _photoService = photoService;
            _sanitizerService = sanitizerService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _mediaContentRepository.GetAll();

            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchString, string sortOrder, MovieTypes[] movieTypes, Genre[] movieGenres)
        {
            var movies = await _mediaContentRepository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = _sanitizerService.SanitizeHtml(searchString);
                movies = await _mediaContentRepository.GetBySearch(searchString);
            }

            switch (sortOrder)
            {
                case "Rating":
                    movies = movies.OrderByDescending(x => x.Rating).ToList();
                    break;

                case "Duration":
                    movies = movies.OrderByDescending(x => x.Duration).ToList();
                    break;
            }

            if (movieGenres != null && movieGenres.Length > 0)
            {
                movies = movies.Where(s => movieGenres.Contains(s.Genre));
            }

            if (movieTypes != null && movieTypes.Length > 0)
            {
                movies = movies.Where(s => movieTypes.Contains(s.Type));
            }

            return View(movies);
        }

        public async Task<IActionResult> Detail(int id)
        {
            MediaContent media = await _mediaContentRepository.GetByIdAsync(id);
            return View(media);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create()
        {
            CreateMovieViewModel createMovieViewModel = new CreateMovieViewModel();
            return View(createMovieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create(CreateMovieViewModel createMovieViewModel)
        {
            if(ModelState.IsValid == true)
            {
                var result = await _photoService.AddPhotoAsync(createMovieViewModel.Image);

                var mediaContent = new MediaContent
                {
                    Name = _sanitizerService.SanitizeHtml(createMovieViewModel.Name),
                    Overview = _sanitizerService.SanitizeHtml(createMovieViewModel.Overview),
                    Rating = createMovieViewModel.Rating,
                    Duration = createMovieViewModel.Duration,
                    PosterImageUrl = result.Url.ToString(),
                    ImagePublicId = result.PublicId,
                    Type = createMovieViewModel.Type,
                    Genre = createMovieViewModel.Genre,
                };

                _mediaContentRepository.Add(mediaContent);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(createMovieViewModel);       
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _mediaContentRepository.GetByIdAsync(id);

            if (movie == null) return View("Error");

            var editMovieViewModel = new EditMovieViewModel
            {
                Name = movie.Name,
                Overview = movie.Overview,
                Rating = movie.Rating,
                Duration = movie.Duration,
                PosterImageUrl = movie.PosterImageUrl,
                Type = movie.Type,
                Genre = movie.Genre,
            };

            return View(editMovieViewModel);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id, EditMovieViewModel editMovieViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit movie");
                return View("Edit", editMovieViewModel);
            }

            var existingMovie = await _mediaContentRepository.GetByIdNoTrackingAsync(id);

            if (existingMovie != null)
            {
                if(editMovieViewModel.Image != null)
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync(existingMovie.ImagePublicId);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete photo");

                        return View(editMovieViewModel);
                    }

                    var photoResult = await _photoService.AddPhotoAsync(editMovieViewModel.Image);

                    var mediaContent = new MediaContent
                    {
                        Id = existingMovie.Id,
                        Name = _sanitizerService.SanitizeHtml(editMovieViewModel.Name),
                        Overview = _sanitizerService.SanitizeHtml(editMovieViewModel.Overview),
                        Rating = editMovieViewModel.Rating,
                        Duration = editMovieViewModel.Duration,
                        PosterImageUrl = photoResult.Url.ToString(),
                        ImagePublicId = photoResult.PublicId,
                        Type = editMovieViewModel.Type,
                        Genre = editMovieViewModel.Genre,
                    };

                    _mediaContentRepository.Update(mediaContent);
                }    
                
                return RedirectToAction("Index");
            }

            else return View(editMovieViewModel);           
        }

        [HttpDelete]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _mediaContentRepository.GetByIdAsync(id);

            if (movie == null) return View("Error");

            if(movie.ImagePublicId != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(movie.ImagePublicId);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");

                    return View("Error");
                }
            }

            _mediaContentRepository.Delete(movie);  
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PutIntoDb()
        {

            string jsonString = await TMDBApiCaller.GetMoviesAsync(1);
            var root = JsonConvert.DeserializeObject<Root>(jsonString);
            List<MovieLink> movieLinks = root.results;
            List<MovieToDb> movieToDbs = new List<MovieToDb>();

            foreach (var movie in movieLinks)
            {
                string movieByIdJson = await TMDBApiCaller.GetByIdAsync(movie.id);
                var movieToDb = JsonConvert.DeserializeObject<MovieToDb>(movieByIdJson);
                movieToDbs.Add(movieToDb);
            }

            foreach (var movie in movieToDbs)
            {
                var mediaContent = new MediaContent
                {
                    Name = movie.title,
                    Overview = movie.tagline,
                    Rating = movie.vote_average,
                    Duration = TimeSpanConverter.ConvertMinutesToTimeSpan(movie.runtime),
                    PosterImageUrl = $"https://image.tmdb.org/t/p/w500/{movie.poster_path}",
                    ImagePublicId = "",
                    Type = MovieTypes.Movie,
                    Genre = GenreMapper.MapGenres(movie.genres)
                };

                _mediaContentRepository.Add(mediaContent);

            }

            return RedirectToAction("Index");
        }
    }
}
