using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DoctorModels
{
    public class PatientDto1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
       // public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
       // public ICollection<MedicalRecord> MedicalRecords { get; set; } = new HashSet<MedicalRecord>();


    }
}
