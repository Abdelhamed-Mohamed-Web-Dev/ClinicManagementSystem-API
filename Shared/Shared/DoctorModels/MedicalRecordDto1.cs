
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DoctorModels
{
    public class MedicalRecordDto1
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public string Diagnoses { get; set; }
        public string Prescription { get; set; }
        public string Speciality { get; set; }
        public String PatientName { get; set; }
      //  public ICollection<LapTestDto> LapTestDtos { get; set; } = new List<LapTestDto>();
       public ICollection<LapTestDto1> LapTestDto { get; set; } = new List<LapTestDto1>();
        public ICollection<RadiologyDto1> RadiationDto { get; set; } = new HashSet<RadiologyDto1>();

    }
}
