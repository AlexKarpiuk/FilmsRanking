using FilmsRanking.Interfaces;
using FilmsRanking.Models;
using FilmsRanking.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FilmsRanking.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMediaContentRepository _mediaContentRepository;

        public MovieController(IMediaContentRepository mediaContentRepository)
        {
            _mediaContentRepository = mediaContentRepository;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _mediaContentRepository.GetAll();
            return View(movies);
        }

        public async Task<IActionResult> Detail(int id)
        {
            MediaContent media = await _mediaContentRepository.GetByIdAsync(id);
            return View(media);
        }

        public IActionResult Create() 
        { 
            CreateMovieViewModel createMovieViewModel = new CreateMovieViewModel();
            return View(createMovieViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateMovieViewModel createMovieViewModel)
        {
            if(ModelState.IsValid == true)
            {
                var mediaContent = new MediaContent
                {
                    Name = createMovieViewModel.Name,
                    Director = createMovieViewModel.Director,
                    Rating = createMovieViewModel.Rating,
                    Duration = createMovieViewModel.Duration,
                    PosterImageUrl = createMovieViewModel.PosterImageUrl,
                    Type = createMovieViewModel.Type,
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

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _mediaContentRepository.GetByIdAsync(id);

            if (movie == null) return View("Error");

            var editMovieViewModel = new EditMovieViewModel
            {
                Name = movie.Name,
                Director = movie.Director,
                Rating = movie.Rating,
                Duration = movie.Duration,
                PosterImageUrl = movie.PosterImageUrl,
                Type = movie.Type,
            };

            return View(editMovieViewModel);
        }

        [HttpPost]
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
                var mediaContent = new MediaContent
                {
                    Id = existingMovie.Id,
                    Name = editMovieViewModel.Name,
                    Director = editMovieViewModel.Director,
                    Rating = editMovieViewModel.Rating,
                    Duration = editMovieViewModel.Duration,
                    PosterImageUrl = editMovieViewModel.PosterImageUrl,
                    Type = editMovieViewModel.Type,
                };

                _mediaContentRepository.Update(mediaContent);
                return RedirectToAction("Index");
            }

            else return View(editMovieViewModel);           
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _mediaContentRepository.GetByIdAsync(id);

            if (movie == null) return View("Error");

            _mediaContentRepository.Delete(movie);  
            return RedirectToAction("Index");
        }
    }
}
