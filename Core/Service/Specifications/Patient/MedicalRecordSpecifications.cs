
namespace Service.Specifications.Patient
{
	public class MedicalRecordSpecifications : BaseSpecifications<MedicalRecord>
	{
		public MedicalRecordSpecifications(int patientId)
			: base(r=>r.PatientId == patientId)
		{
			AddInclude(r => r.PatientId);
			AddInclude(r => r.Doctor);
		}
		public MedicalRecordSpecifications(Guid recordId)
			: base(r => r.Id == recordId)
		{
			AddInclude(r => r.Patient);
			AddInclude(r => r.Doctor);
		}
	}
}
