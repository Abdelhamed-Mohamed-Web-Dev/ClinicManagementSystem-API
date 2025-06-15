using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AdminModels
{
	public record UpdateDoctorDto
	{
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Bio { get; set; }
		[Required]
		public decimal NewVisitPrice { get; set; }
		[Required]
		public decimal FollowUpVisitPrice { get; set; }
		[Required]
		// { 0 => "Sunday",	1 => "Monday", ..., 6 => "Saturday" }
		public ICollection<int> WorkingDays { get; set; } = new List<int>();
		[Required]
		public TimeSpan StartTime { get; set; } = new TimeSpan(13, 0, 0); // 1 PM default value
		[Required]
		public TimeSpan EndTime { get; set; } = new TimeSpan(20, 0, 0); // 8 PM	default value
		[Required]
		public int AppointmentDuration { get; set; } = 30; // 30 Minutes default value

	}
}
