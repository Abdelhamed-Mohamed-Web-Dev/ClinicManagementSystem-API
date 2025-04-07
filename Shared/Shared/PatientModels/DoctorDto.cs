using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.PatientModels
{
	public record DoctorDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Speciality { get; set; }

		//public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
	}
}
