using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Models
{
    public class AuthenticationResult
    {

        public  string? token { get; set; }
        public IEnumerable<string>? ErrorMessages { get; set; }
        public bool success { get; set; }
    }
}
