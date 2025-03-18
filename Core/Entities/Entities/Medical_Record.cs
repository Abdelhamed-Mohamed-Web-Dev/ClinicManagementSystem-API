using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Medical_Record
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string Diagnoses { get; set; }
        public string Prescription { get; set; }
        public string Speciality { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public ICollection<Lap_Test> lap_Tests { get; set; } = new List<Lap_Test>();
        public ICollection<Radiology> Radioizations { get; set; } = new HashSet<Radiology>();
    }
}
