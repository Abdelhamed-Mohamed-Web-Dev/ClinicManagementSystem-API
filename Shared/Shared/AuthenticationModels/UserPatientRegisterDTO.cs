using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationModels
{
    public record UserPatientRegisterDTO : UserRegisterDTO
    {
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string? Address { get; set; }
    }
}
