namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IPatientService> patientService;
		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
		{
			patientService = new Lazy<IPatientService>(() => new PatientService.PatientService(unitOfWork, mapper));


		}
		public IPatientService PatientService() => patientService.Value;
	}
}
