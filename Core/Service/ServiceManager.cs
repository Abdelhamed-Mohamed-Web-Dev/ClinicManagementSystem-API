using Service.Abstraction.DoctorService;

namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IPatientService> patientService;
		readonly Lazy<IDoctorService> doctorService;
		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
		{
			patientService = new Lazy<IPatientService>(() => new PatientService.PatientService(unitOfWork, mapper));
			doctorService = new Lazy<IDoctorService>(()=> new DoctorService.DoctorService(unitOfWork, mapper));

		}
		public IPatientService PatientService() => patientService.Value;
        public IDoctorService DoctorService() => doctorService.Value;
	}
}
