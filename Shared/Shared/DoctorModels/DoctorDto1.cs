using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DoctorModels
{
    public class DoctorDto1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
        public ICollection<AppointmentDto1> AppointmentsDto { get; set; } = new List<AppointmentDto1>();
    }
}
