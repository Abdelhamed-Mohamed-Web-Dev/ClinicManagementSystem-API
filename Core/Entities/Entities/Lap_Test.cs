using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lap_Test
    {
        public int Id { get; set; }
        public string TestType { get; set; }
        public DateOnly TestDate { get; set; }
        public string TestResult { get; set; }
        public int TestUnits { get; set; }
        public string Comments { get; set; }
        public string LapName { get; set; }
        public int ReferenseRange { get; set; }
        public Medical_Record Medical_Record { get; set; }
        public int MedicalId { get; set; }
    }
}
