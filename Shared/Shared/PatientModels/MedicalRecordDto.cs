
namespace Shared.PatientModels
{
	public record MedicalRecordDto
	{
		public Guid Id { get; set; }
		public DateOnly Date { get; set; }
		public string Diagnoses { get; set; }
		public string Prescription { get; set; }
		public string Speciality { get; set; }
		public string DoctorName { get; set; } //

		//public int DoctorId { get; set; }
		public string PatientName { get; set; } //

		//public int PatientId { get; set; }

		public ICollection<LapTestDto> LapTests { get; set; } = new List<LapTestDto>();
		public ICollection<RadiologyDto> Radiation { get; set; } = new HashSet<RadiologyDto>();
	}
}
