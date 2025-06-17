using ClinicManagementSystem.Helpers;
using Shared.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AuthenticationController(IServiceManager serviceManager ) : APIController
    {
        //[HttpPost("Login")]
        //public async Task<ActionResult<UserResultDTO>> Login(UserLoginDTO loginDTO)
        //{
        //    var result = (await serviceManager.AuthenticationService().LoginAsycn(loginDTO));
        //    return Ok(result);
        //}
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(UserLoginDTO login)
            => Ok(await serviceManager.AuthenticationService().LoginAsync(login));


        //[HttpPost("Register")]
        //public async Task<ActionResult<UserRegisterDTO>> Register( [FromBody] UserPatientRegisterDTO registerDTO)
        //{
        //    var result = await serviceManager.AuthenticationService().PatientRegisterAsync(registerDTO);
        //    return Ok(result);
        //}

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(string email)
        {
            await serviceManager.AuthenticationService().DeleteAsync(email);
            return Ok("User deleted successfully");
        }

    }
}
