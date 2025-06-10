using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Abstraction.AuthenticationService;
using Service.Abstraction.DoctorService;
using Service.AuthenticationService;
using Shared;

namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IPatientService> patientService;
		readonly Lazy<IDoctorService> doctorService;
		readonly Lazy<IAuthenticationService> authenticationService;

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,UserManager<User> userManager,IOptions<JwtOptions> options)
		{
			patientService = new Lazy<IPatientService>(() => new PatientService.PatientService(unitOfWork, mapper));
			doctorService = new Lazy<IDoctorService>(()=> new DoctorService.DoctorService(unitOfWork, mapper));
            authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService.AuthenticationService(userManager,options) );

		}
		public IPatientService PatientService() => patientService.Value;
        public IDoctorService DoctorService() => doctorService.Value;

        IAuthenticationService IServiceManager.AuthenticationService()=>authenticationService.Value;
        
    }
}
