
using Service.Abstraction.NotificationService;

namespace Service.Abstraction
{
	public interface IServiceManager
	{
		public IPatientService PatientService();
		public IDoctorService DoctorService();
		public IAdminService AdminService();
		public INotificationService NotificationService();
		public IAuthenticationService AuthenticationService();
		public IPaymentService PaymentService();
	}
}
