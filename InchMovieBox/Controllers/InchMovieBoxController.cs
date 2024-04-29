using InchMovieBox.Application;
using InchMovieBox.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;

namespace InchMovieBox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InchMovieBoxController : ControllerBase
    {
        private readonly IMovieBoxService _movieBoxService;
        private static List<string> SearchHistory = new List<string>();

        public InchMovieBoxController(IMovieBoxService movieBoxService)
        {
            _movieBoxService = movieBoxService;
        }

        [HttpGet("title")]
        public async Task<IActionResult> SearchMovies([FromQuery] string title)
        {
            try
            {
                var movieBoxSearch = await _movieBoxService.SearchMoviesAsync(title);
                if (movieBoxSearch != null)
                {
                    SearchHistory.Insert(0, title);
                    if (SearchHistory.Count > 5)
                        SearchHistory.RemoveAt(5);

                    return Ok(movieBoxSearch);

                }
                else
                {
                    return NotFound("No results found");
                }
            }
            catch (Exception)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while searching for movies.");
            }
        }

        [HttpGet("history")]
        public ActionResult<IEnumerable<string>> GetLastFiveSearches()
        {

            return Ok(SearchHistory);
        }

        
        [HttpGet("{imdbId}")]
        public async Task<IActionResult> MovieDetails(string imdbId)
        {
            try
            {
                var movieBoxDetails = await _movieBoxService.MoviesBoxDetailsAsync(imdbId);
                if (movieBoxDetails != null)
                {
                  
                    return Ok(movieBoxDetails);

                }
                else
                {
                    return NotFound("No results found");
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while searching for movies.");
            }
        }


    }
}
