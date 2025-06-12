using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DoctorModels
{
    public class RadiologyDto1
    {
        public string ImagingType { get; set; }
        public DateOnly ImagingDate { get; set; }
        public string ImagingResult { get; set; }
        public string Comments { get; set; }
        public Guid MedicalRecordId { get; set; }
        
        
    }
}
