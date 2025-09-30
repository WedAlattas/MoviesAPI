using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Database
{
    public class DbInitializer
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DbInitializer(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            await connection.ExecuteAsync("""
            CREATE SCHEMA if not exists moviesdb;
            """);

            await connection.ExecuteAsync("""
            SET search_path TO moviesdb;
            """);
            await connection.ExecuteAsync("""
            create table if not exists movies (
            id UUID primary key,
            title TEXT not null,
            yearofrelease integer not null);
        """);

       
            await connection.ExecuteAsync("""
            create table if not exists genres (
            movieId UUID references movies (Id),
            name TEXT not null);
        """);

            await connection.ExecuteAsync("""
            create table if not exists users (
            id UUID primary key,
            password TEXT not null, 
            email TEXT not null);
        """);
        }
    }
}