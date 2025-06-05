using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Service.Abstraction.AuthenticationService;
using Shared.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthenticationService
{
    public class AuthenticationService(UserManager<User> userManager) : IAuthenticationService
    {
        public async Task<UserResultDTO> LoginAsycn(UserLoginDTO userLogin)
        {
            var user = await userManager.FindByEmailAsync(userLogin.Email);
            if (user == null) throw new UnAuthorizedException("Email Dosen't Exist");

            var result = await userManager.CheckPasswordAsync(user, userLogin.Password);
            if(!result) throw new UnAuthorizedException();

            return new UserResultDTO
            (
                Email: user.Email,
                DispalyName: user.DisplayName,
                Token: "Token"
                );
            
        }

        public async Task<UserResultDTO> RegisterAsycn(UserRegisterDTO userRegister)
        {
            var user = new User()
            {
                Email = userRegister.Email,
                DisplayName = userRegister.DisplayName,
                UserName = userRegister.DisplayName,
                PhoneNumber = userRegister.PhoneNumber,
            };

            var result = await userManager.CreateAsync(user, userRegister.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e=>e.Description).ToList();

                throw new ValidationException(errors);
            }

            return new UserResultDTO
                 (
                     Email: user.Email,
                     DispalyName: user.DisplayName,
                     Token: "Token"
                     );
        }
    }
}
