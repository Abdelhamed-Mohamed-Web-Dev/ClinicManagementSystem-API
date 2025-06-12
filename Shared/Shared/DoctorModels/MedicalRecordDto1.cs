
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
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

      //  public ICollection<LapTestDto> LapTestDtos { get; set; } = new List<LapTestDto>();
       public ICollection<LapTestDto1> LapTestsDto { get; set; } = new List<LapTestDto1>();
        public ICollection<RadiologyDto1> RadiationsDto { get; set; } = new List<RadiologyDto1>();

    }
}
