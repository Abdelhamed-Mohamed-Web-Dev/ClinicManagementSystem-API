using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationModels
{
    public class GooglePayload
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

    }
}
