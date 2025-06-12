
using Service.Abstraction.AuthenticationService;
using Service.Abstraction.DoctorService;

namespace Service.Abstraction
{
	public interface IServiceManager
	{
		public IPatientService PatientService();
		public IDoctorService DoctorService();
		public IAuthenticationService AuthenticationService();
	}
}
