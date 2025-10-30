using Dapper;
using Movies.Application.Database;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        IDbConnectionFactory _dbConnectionFactory;
        
        public IdentityRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<bool> CreateAsync(User user, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
            insert into moviesdb.users (id, password, email, isAdmin) 
            values (@Id, @Password, @Email, @isAdmin)
            """, user, cancellationToken: token));

            transaction.Commit();

            return result > 0;
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
            select count(1) from moviesdb.users where email = @email
            """, new { email }, cancellationToken: token));
        }

        public async Task<User?> GetUserByEmail(string email, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            var user = await connection.QuerySingleOrDefaultAsync<User>(
                new CommandDefinition("""
            select * from moviesdb.users where email = @email
            """, new { email }, cancellationToken: token));

            return user;
        }
    }
}
