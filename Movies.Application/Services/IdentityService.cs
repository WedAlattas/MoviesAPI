using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<AuthenticationResult> LoginAsync(User user, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(User user, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
