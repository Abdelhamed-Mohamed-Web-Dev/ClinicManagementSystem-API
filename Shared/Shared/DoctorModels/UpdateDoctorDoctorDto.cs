using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DoctorModels
{
	public record UpdateDoctorDoctorDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[EmailAddress]
		public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        [EmailAddress]
		public string OldEmail { get; set; }
		public string Speciality { get; set; }
		public string Bio { get; set; }
		public string Phone { get; set; }
		public ICollection<int> WorkingDays { get; set; } = new List<int>();
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public int AppointmentDuration { get; set; }

	}
}
