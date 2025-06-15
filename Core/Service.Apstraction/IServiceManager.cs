
namespace Service.Abstraction
{
	public interface IServiceManager
	{
		public IPatientService PatientService();
		public IDoctorService DoctorService();
		public IAdminService AdminService();
		public IAuthenticationService AuthenticationService();
		public IPaymentService PaymentService();
	}
}
