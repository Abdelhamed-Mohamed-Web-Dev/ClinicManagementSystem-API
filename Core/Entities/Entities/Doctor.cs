
namespace Domain.Entities
{
    public class Doctor : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
        // محتاجين نضيف Notification ( 2 Variable ( Type ,Content)   )
        public string PictureUrl { get; set; }
        public int Rate { get; set; }
        public string Bio { get; set; }
        public decimal NewVisitPrice { get; set; }
        public decimal FollowUpVisitPrice { get; set; }

        // { 0 => "Sunday",	1 => "Monday", ..., 6 => "Saturday" }
        public ICollection<int> WorkingDays { get; set; } = new List<int>();
        public TimeSpan StartTime { get; set; } = new TimeSpan(13, 0, 0); // 1 PM
        public TimeSpan EndTime { get; set; } = new TimeSpan(20, 0, 0); // 8 PM
		public int AppointmentDuration { get; set; } = 30; // 30 Minutes

		public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
		
	}
}
