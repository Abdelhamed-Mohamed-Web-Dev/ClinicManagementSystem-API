
namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IPatientService> patientService;
		readonly Lazy<IDoctorService> doctorService;
		readonly Lazy<IAdminService> adminService;
		readonly Lazy<IAuthenticationService> authenticationService;
		readonly Lazy<IPaymentService> paymentService;

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IOptions<JwtOptions> options, IConfiguration configuration)
		{
			patientService = new Lazy<IPatientService>(() => new PatientService.PatientService(unitOfWork, mapper));
			doctorService = new Lazy<IDoctorService>(() => new DoctorService.DoctorService(unitOfWork, mapper));
			adminService = new Lazy<IAdminService>(() => new AdminService.AdminService(unitOfWork, mapper,userManager));
			paymentService = new Lazy<IPaymentService>(() => new PaymentService(unitOfWork, configuration, mapper));
			authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService.AuthenticationService(userManager, options));
		}
		public IPatientService PatientService() => patientService.Value;
		public IDoctorService DoctorService() => doctorService.Value;
		public IAdminService AdminService() => adminService.Value;
		public IPaymentService PaymentService() => paymentService.Value;
		IAuthenticationService IServiceManager.AuthenticationService() => authenticationService.Value;
	}
}
