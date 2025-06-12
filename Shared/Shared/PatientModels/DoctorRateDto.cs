using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.PatientModels
{
   public record DoctorRateDto 
    {
     //   public int Id { get; set; }
        public int DoctorId { get; set; }
     //   public string DoctorName { get; set; }
        public int PatientId { get; set; }
     //   public string PatientName { get; set; }
        public Guid? AppointmentId { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
