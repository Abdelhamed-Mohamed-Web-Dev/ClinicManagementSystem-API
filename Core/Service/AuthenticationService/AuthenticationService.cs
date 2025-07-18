﻿using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Abstraction.AuthenticationService;
using Shared;
using Shared.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthenticationService
{
    public class AuthenticationService(UserManager<User> userManager,IOptions<JwtOptions> options) : IAuthenticationService
    {
        public async Task DeleteAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

          //  if (user == null)
         //       throw new NotFoundException("User not found");

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
        }

        public async Task<UserResultDTO> LoginAsync(UserLoginDTO userLogin)
        {
            var user = await userManager.FindByEmailAsync(userLogin.Email);
            if (user == null) throw new UnAuthorizedException("Email Dosen't Exist");

            var result = await userManager.CheckPasswordAsync(user, userLogin.Password);
            if(!result) throw new UnAuthorizedException();
            return new UserResultDTO(
                user.DisplayName,
                user.Email,
                await CreateTokenAsync(user));
            
            return new UserResultDTO
            (
                Email: user.Email,
                DisplayName: user.DisplayName,
                Token: "Token"
                );
            
        }

        public async Task<UserResultDTO> RegisterAsync(UserRegisterDTO userRegister)
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
            await userManager.AddToRoleAsync(user, userRegister.Role);


            return new UserResultDTO(
                         user.DisplayName,
                         user.Email,
                         await CreateTokenAsync(user));
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var Jwtoptions = options.Value;
            // Private Claims
            var authclaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.Email,user.Email!)
            };

            // Add Role To Claim If Exist
    
            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
                authclaim.Add(new Claim("role", role));

            //          foreach (var role in roles)
            //            authclaim.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtoptions.SecretKey));
     
            var signingcreds =new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                audience: Jwtoptions.Audience,
                issuer: Jwtoptions.Issure,
                expires:DateTime.UtcNow.AddDays(Jwtoptions.DurationInDays),
                claims : authclaim,
                signingCredentials: signingcreds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
