
namespace Service.Specifications.Patient
{
	public class MedicalRecordSpecifications : Specifications<MedicalRecord>
	{
		public MedicalRecordSpecifications(int patientId)
			: base(r => r.PatientId == patientId)
		{
			AddInclude(r => r.Patient);
			AddInclude(r => r.Doctor);
			AddInclude(r => r.LapTests);
			AddInclude(r => r.Radiation);
		}
		public MedicalRecordSpecifications(Guid recordId)
			: base(r => r.Id == recordId)
		{
			AddInclude(r => r.Patient);
			AddInclude(r => r.Doctor);
			AddInclude(r => r.LapTests);
			AddInclude(r => r.Radiation);
		}
	}
}
