using Movies.Application.Models;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.API.Mapping
{
    public static class ContractMapping
    {

        public static Movie MaptoMovie(this CreateMovieRequest request)
        {
            return new Movie
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres?.ToList() ?? new List<string>()
            };
        }

        public static Movie MaptoMovie(this UpdateMovieRequest request, Guid id)
        {
            return new Movie
            {
                Id = id,
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres?.ToList() ?? new List<string>()
            };
        }
        public static MovieResponse MapToResponse(this Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Genres = movie.Genres ?? Enumerable.Empty<string>()
            };



        }
        public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies)
        {
            return new MoviesResponse
            {
                Items = movies.Select(MapToResponse)
            };
        }

        public static User MapToUser(this UserRegisterationRequest user)
        {
            return new User() { 
                Email = user.Email, 
                Password = user.Password, 
                Id = Guid.NewGuid(), isAdmin = user.isAdmin
            };
        }

        public static User MapToUser(this LoginRequest user)
        {
            return new User()
            {
                Email = user.Email,
                Password = user.Password,
            };
        }

    }
}