using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string>? ErrorMessages { get; set; }
    }
}
