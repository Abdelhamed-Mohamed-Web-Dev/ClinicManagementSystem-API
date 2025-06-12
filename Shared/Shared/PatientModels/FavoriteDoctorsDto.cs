using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.PatientModels
{
   public record FavoriteDoctorsDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

    }
}
