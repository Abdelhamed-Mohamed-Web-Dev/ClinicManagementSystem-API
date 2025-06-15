using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AdminModels
{
	public record UserDoctorDto
	{
		// user
		[Required(ErrorMessage = "DisplayName is Required")]
		public string DisplayName { get; set; }
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		public string Password { get; set; }
		[Required(ErrorMessage = "PhoneNumber is Required")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "UserName is Required")]
		public string UserName { get; set; }
		// doctor
        [Required]
		public string Name { get; set; }
        [Required]
		public string Speciality { get; set; }
        [Required]
		public string? PictureUrl { get; set; }
        [Required]
		public string? Bio { get; set; }
        [Required]
		public decimal NewVisitPrice { get; set; }
        [Required]
		public decimal FollowUpVisitPrice { get; set; }
		// { 0 => "Sunday",	1 => "Monday", ..., 6 => "Saturday" }
        [Required]
		public ICollection<int> WorkingDays { get; set; } = new List<int>();
        [Required]
		public TimeSpan StartTime { get; set; } = new TimeSpan(13, 0, 0); // 1 PM default value
        [Required]
		public TimeSpan EndTime { get; set; } = new TimeSpan(20, 0, 0); // 8 PM	default value
        [Required]
		public int AppointmentDuration { get; set; } = 30; // 30 Minutes default value


	}
}
