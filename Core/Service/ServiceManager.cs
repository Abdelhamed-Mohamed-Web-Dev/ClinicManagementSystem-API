using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Abstraction.AuthenticationService;
using Service.Abstraction.DoctorService;
using Service.AuthenticationService;
using Service.PatientService;
using Shared;

namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IPatientService> patientService;
		readonly Lazy<IDoctorService> doctorService;
		readonly Lazy<IAuthenticationService> authenticationService;
		readonly Lazy<IPaymentService> paymentService;

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IOptions<JwtOptions> options, IConfiguration configuration)
		{
			patientService = new Lazy<IPatientService>(() => new PatientService.PatientService(unitOfWork, mapper));
			doctorService = new Lazy<IDoctorService>(() => new DoctorService.DoctorService(unitOfWork, mapper));
			authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService.AuthenticationService(userManager, options,unitOfWork));
			paymentService = new Lazy<IPaymentService>(() => new PaymentService(unitOfWork, configuration, mapper));
		}
		public IPatientService PatientService() => patientService.Value;
		public IDoctorService DoctorService() => doctorService.Value;

		IAuthenticationService IServiceManager.AuthenticationService() => authenticationService.Value;

		public IPaymentService PaymentService() => paymentService.Value;
	}
}
