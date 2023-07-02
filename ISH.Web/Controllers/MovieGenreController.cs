using ISH.Service;
using ISH.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/movie-genre")]
    public class MovieGenreController : ControllerBase
    {
        private readonly IMovieGenreService _movieGenreService;

        public MovieGenreController(IMovieGenreService movieGenreService)
        {
            _movieGenreService = movieGenreService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_movieGenreService.GetAll());

        [HttpPost]
        public IActionResult Create([FromQuery] string movieGenre) => Ok(_movieGenreService.Create(movieGenre));

        [HttpPut]
        public IActionResult Update([FromBody] MovieGenreDto movieGenre) => Ok(_movieGenreService.Update(movieGenre));
    }
}
