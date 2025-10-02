using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public interface IIdentityRepository
    {
        Task<bool> CreateAsync(User user, CancellationToken token = default);

        Task<User?> GetUserByEmail(string email, CancellationToken token = default);

        Task<bool> ExistsByEmailAsync(string email, CancellationToken token = default);
    }
}
