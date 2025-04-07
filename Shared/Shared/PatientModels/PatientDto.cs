using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.PatientModels
{
	public record PatientDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public DateOnly BirthDate { get; set; }
		public string Gender { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public ICollection<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
		public ICollection<MedicalRecordDto> MedicalRecords { get; set; } = new HashSet<MedicalRecordDto>();

	}
}
