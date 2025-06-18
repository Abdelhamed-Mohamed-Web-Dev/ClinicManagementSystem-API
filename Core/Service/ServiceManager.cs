
using ClinicManagementSystem.Helpers;
using Service.Abstraction.NotificationService;

namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IPatientService> patientService;
		readonly Lazy<IDoctorService> doctorService;
		readonly Lazy<IAdminService> adminService;
		readonly Lazy<INotificationService> notificationService;
		readonly Lazy<IAuthenticationService> authenticationService;
		readonly Lazy<IPaymentService> paymentService;

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IOptions<JwtOptions> options, IConfiguration configuration,HttpClient httpClient)
		{
			patientService = new Lazy<IPatientService>(() => new PatientService.PatientService(unitOfWork, mapper));
			doctorService = new Lazy<IDoctorService>(() => new DoctorService.DoctorService(unitOfWork, mapper, userManager));
			adminService = new Lazy<IAdminService>(() => new AdminService.AdminService(unitOfWork, mapper, userManager));
			notificationService = new Lazy<INotificationService>(() => new NotificationService.NotificationService(unitOfWork, mapper));
			paymentService = new Lazy<IPaymentService>(() => new PaymentService(unitOfWork, configuration, mapper));
			authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService.AuthenticationService(userManager, options,httpClient));
		}
		public IPatientService PatientService() => patientService.Value;
		public IDoctorService DoctorService() => doctorService.Value;
		public IAdminService AdminService() => adminService.Value;
		public INotificationService NotificationService() => notificationService.Value;
		public IPaymentService PaymentService() => paymentService.Value;
		public IAuthenticationService AuthenticationService() => authenticationService.Value;
		IAuthenticationService IServiceManager.AuthenticationService() => authenticationService.Value;
	}
}
