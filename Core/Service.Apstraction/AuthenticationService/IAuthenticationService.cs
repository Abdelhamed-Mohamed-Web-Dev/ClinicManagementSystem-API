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
        public Task<UserResultDTO> LoginAsycn(UserLoginDTO userLogin);
        public Task<UserResultDTO> RegisterAsycn(UserRegisterDTO userRegister);
    }
}
