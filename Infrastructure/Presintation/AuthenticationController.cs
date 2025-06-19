using ClinicManagementSystem.Helpers;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Shared.AdminModels;
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

        [HttpPost("google-login")]
        public async Task<IActionResult> LoginWithGoogle([FromQuery] string IdToken, string? username)
        {
            try
            {
                var user = await serviceManager.AuthenticationService().LoginWithGoogleAsync(IdToken,username);
                return Ok(user); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("RegistePatientByGoogle")]
        public async Task<IActionResult> AddPatientByGoogle([FromBody] UserPatientDto userPatientDto )
            => Ok(await serviceManager.AuthenticationService().AddPatientByGoogle(userPatientDto));

        
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(string email)
        {
            await serviceManager.AuthenticationService().DeleteAsync(email);
            return Ok("User deleted successfully");
        }

    }
}
