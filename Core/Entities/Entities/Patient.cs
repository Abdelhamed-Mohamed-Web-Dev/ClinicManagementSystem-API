
namespace Domain.Entities
{
    public class Patient : BaseEntity<int>
	{
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new HashSet<MedicalRecord>();

    }
}
