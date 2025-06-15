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
        public Task<UserResultDTO> DoctorRegisterAsync(UserDoctorRegisterDTO userRegister);
        public Task<UserResultDTO> PatientRegisterAsync(UserPatientRegisterDTO userRegister);
        public Task<UserResultDTO> AdminRegisterAsync(UserAdminRegisterDTO userRegister);
        public Task DeleteAsync(string email);
    }
}
