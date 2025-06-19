using Shared.AdminModels;
using Shared.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.AuthenticationService
{
    public interface IAuthenticationService
    {
        public Task<UserResultDTO> LoginAsync(UserLoginDTO userLogin);
        public Task<UserResultDTO> RegisterAsync(UserRegisterDTO userRegister);
        public Task DeleteAsync(string email);
        public Task<UserResultLoginDTO> LoginWithGoogleAsync(string IdToken, string? username);
        public Task<UserPatientDto> AddPatientByGoogle(UserPatientDto _patient);
    }
}
