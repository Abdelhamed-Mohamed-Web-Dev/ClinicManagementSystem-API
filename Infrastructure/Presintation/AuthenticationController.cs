using Shared.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AuthenticationController(IServiceManager serviceManager) : APIController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(UserLoginDTO loginDTO)
        {
            var result = (await serviceManager.AuthenticationService().LoginAsycn(loginDTO));
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDTO>> Register(UserRegisterDTO registerDTO)
        {
            var result = await serviceManager.AuthenticationService().RegisterAsycn(registerDTO);
            return Ok(result);
        }
    }
}
