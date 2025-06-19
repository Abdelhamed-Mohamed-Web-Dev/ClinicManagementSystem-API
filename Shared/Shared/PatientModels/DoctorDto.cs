using Shared.AppointmentModels;

namespace Shared.PatientModels
{
	public record DoctorDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
		public string Speciality { get; set; }
		public string PictureUrl { get; set; }
		public double Rate { get; set; }
		public string Bio { get; set; }
		public decimal NewVisitPrice { get; set; }
		public decimal FollowUpVisitPrice { get; set; }
		public ICollection<int> WorkingDays { get; set; } = new List<int>();
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; } 
		public int AppointmentDuration { get; set; }
		//public ICollection<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
	}
}
