
namespace Service.Specifications.Patient
{
	public class AppointmentSpecifications : Specifications<Appointment>
	{
		public AppointmentSpecifications() : // to get upcoming appointments
			base(a => a.AppointmentDateTime.Date > DateTime.Now.Date.AddDays(1))
		{
			AddInclude(a => a.Patient);
			AddInclude(a => a.Doctor);
		}
		public AppointmentSpecifications(int patientId) : // to get appointment for patient
			base(a => a.PatientId == patientId)
		{
			AddInclude(a => a.Patient);
			AddInclude(a => a.Doctor);
		}
		public AppointmentSpecifications(Guid id) : // to get appointment by id
			base(a => a.Id == id)
		{
			AddInclude(a => a.Patient);
			AddInclude(a => a.Doctor);
		}
		public AppointmentSpecifications(string PaymentIntentId)
			: base(a => a.PaymentIntentId == PaymentIntentId)
		{
			AddInclude(a => a.Patient);
			AddInclude(a => a.Doctor);
		}
		public AppointmentSpecifications // to get appointments with filtrations
			(int? doctorId, int? patientId, DateTime? date, AppointmentStatus? status) :
			base(a =>
			(doctorId == null || a.DoctorId == doctorId.Value) &&
			(patientId == null || a.PatientId == patientId.Value) &&
			(date == null || a.AppointmentDateTime.Date == date.Value.Date) &&
			(status == null || a.Status == status.Value)
		)
		{
			AddInclude(a => a.Patient);
			AddInclude(a => a.Doctor);
		}

	}
}
