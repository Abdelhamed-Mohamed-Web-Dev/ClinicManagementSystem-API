
namespace Domain.Entities
{
	public class MedicalRecord : BaseEntity<Guid>
	{
		public DateOnly Date { get; set; }
		public string Diagnoses { get; set; }
		public string Prescription { get; set; }
		public string Speciality { get; set; }
		public Doctor Doctor { get; set; }
		public int DoctorId { get; set; }
		public Patient Patient { get; set; }
		public int PatientId { get; set; }
		public ICollection<LapTest> LapTests { get; set; } = new List<LapTest>();
		public ICollection<Radiology> Radiation { get; set; } = new HashSet<Radiology>();
	}
}
