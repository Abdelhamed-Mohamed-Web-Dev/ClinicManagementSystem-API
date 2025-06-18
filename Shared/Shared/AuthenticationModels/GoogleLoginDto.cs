using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationModels
{
    public class GoogleLoginDto
    {
        public string IdToken { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

    }
}
