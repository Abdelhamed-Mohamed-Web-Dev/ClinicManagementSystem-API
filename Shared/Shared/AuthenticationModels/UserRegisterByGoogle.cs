using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationModels
{
    public class UserRegisterByGoogle
    {
        //        Address = _patient.Address,
        //        BirthDate = _patient.BirthDate,
        //        Phone = _patient.PhoneNumber,
        //        Gender = _patient.Gender,
        //public record UserResultDTO(string DisplayName, string Email, string Token, string UserName);
        public string Address { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

    }
}
