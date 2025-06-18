
namespace Service.Specifications
{
	public class NotificationSpecifications : Specifications<Notifications>
	{
		public NotificationSpecifications(int? doctorId, int? patientId) :
				base(n =>
					(doctorId == null || n.DoctorId == doctorId) &&
					(patientId == null || n.PatientId == patientId))
		{
		}
	}
}
