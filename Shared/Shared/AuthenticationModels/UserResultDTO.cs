using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationModels
{
    public record UserResultDTO(string DisplayName, string Email, string Token,string UserName);
    
    
}
