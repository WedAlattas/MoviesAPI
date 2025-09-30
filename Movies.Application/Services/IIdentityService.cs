using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(User user, CancellationToken token = default);
        Task<bool> RegisterAsync(User user, CancellationToken token = default);


    }
}
