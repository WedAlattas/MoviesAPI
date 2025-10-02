using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Mapping;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace MoviesAPI.Controllers
{
    
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        [HttpPost(ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
        {
            if (request == null)
            {
                return BadRequest("Movie cannot be null.");
            }

            var movie = request.MaptoMovie();

            var result = await _movieRepository.CreateAsync(movie);

            if (result)
            {
                var response = movie.MapToResponse();
                return CreatedAtAction(nameof(Get), new { id = movie.Id }, response);
            }

            return StatusCode(500, "An error occurred while creating the movie.");
        }

        [HttpPost(ApiEndpoints.Movies.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _movieRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var response = result.MapToResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movieRepository.GetAllAsync();

            var moviesResponse = movies.MapToResponse();
            return Ok(moviesResponse);

        }


        [HttpPut(ApiEndpoints.Movies.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id,
           [FromBody] UpdateMovieRequest request)
        {
            var movie = request.MaptoMovie(id);
            var updated = await _movieRepository.UpdateAsync(movie);
            if (!updated)
            {
                return NotFound();
            }

            var response = movie.MapToResponse();
            return Ok(response);
        }


        [HttpDelete(ApiEndpoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _movieRepository.DeleteByIdAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();


        }



    } 
            
            }
