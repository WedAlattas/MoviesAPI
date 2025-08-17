﻿using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<Movie> _movieValidator;

        public MovieService(IMovieRepository movieRepository, IValidator<Movie> moviveValidator)
        {
            _movieRepository = movieRepository;
            _movieValidator = moviveValidator;
        }
        public async Task<bool> CreateAsync(Movie movie, CancellationToken token = default)
        {

            await _movieValidator.ValidateAndThrowAsync(movie, token);
            return await _movieRepository.CreateAsync(movie, token);
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _movieRepository.DeleteByIdAsync(id, token);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
        {
            return await _movieRepository.GetAllAsync(token);
        }

        public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _movieRepository.GetByIdAsync(id, token);
        }

 
        public async Task<Movie?> UpdateAsync(Movie movie, CancellationToken token = default)
        {
            await _movieValidator.ValidateAndThrowAsync(movie, token);
            var movieExists = await _movieRepository.ExistsByIdAsync(movie.Id, token);
            if (!movieExists)
            {
                return null;
            }

            await _movieRepository.UpdateAsync(movie, token);
            return movie;        }
    }
}
