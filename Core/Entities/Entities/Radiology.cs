using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Radiology
    {
        public int Id { get; set; }
        public string ImagingType { get; set; }
        public DateOnly ImagingDate { get; set; }
        public string ImagingResult { get; set; }
        public string Comments { get; set; }
        public Medical_Record medical_Record { get; set; }
        public int MedicalRecord_Id { get; set; }

    }
}
