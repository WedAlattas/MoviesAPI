using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Requests
{
    public class UserRegisterationRequest
    {
        public required string Email { get; init; } 
        public required string Password { get; init; }
        public required bool isAdmin { get; set; }

    }
}
