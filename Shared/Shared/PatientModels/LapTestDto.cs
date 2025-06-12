using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.PatientModels
{
	public record LapTestDto
	{
        public Guid Id { get; set; }
        public string TestType { get; set; }
		public DateOnly TestDate { get; set; }
		public string TestResult { get; set; }
		public int TestUnits { get; set; }
		public string Comments { get; set; }
		public string LapName { get; set; }
		public int ReferenceRange { get; set; }
		//public Guid MedicalId { get; set; }
		//public MedicalRecord MedicalRecord { get; set; }
	}
}
