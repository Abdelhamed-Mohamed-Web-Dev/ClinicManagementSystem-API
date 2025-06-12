

namespace Service.Specifications.Patient
{
	public class AppointmentSpecifications : Specifications<Appointment>
	{
		public AppointmentSpecifications(int patientId) : base(a => a.PatientId == patientId)
		{
			AddInclude(a => a.Patient);
			AddInclude(a => a.Doctor);
		}
		public AppointmentSpecifications(Guid id) : base(a=>a.Id == id)
		{
			AddInclude(a => a.Patient);
			AddInclude(a => a.Doctor);
		}

	}
}
