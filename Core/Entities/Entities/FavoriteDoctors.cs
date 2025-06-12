using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FavoriteDoctors : BaseEntity<int>
    {
        public int PatientId { get; set; }
        public Patient patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor doctor { get; set; }
    }
}
