
namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class PatientController(IServiceManager serviceManager) : ControllerBase
	{
		#region Doctor End Points
		[HttpGet("Doctors")]
		public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors(string? specialty, string? search)
			=> Ok(await serviceManager.PatientService().GetAllDoctorsAsync(specialty, search));
		[HttpGet("Doctors/{id}")]
		public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
			=> Ok(await serviceManager.PatientService().GetDoctorByIdAsync(id));
		#endregion

		#region Medical Record End Points

		[HttpGet("AllMedicalRecords/{patientId}")]
		public async Task<ActionResult<IEnumerable<MedicalRecordDto>>> GetMedicalRecords(int patientId)
			=> Ok(await serviceManager.PatientService().GetAllMedicalRecordsAsync(patientId));
		[HttpGet("MedicalRecords/{id}")]
		public async Task<ActionResult<MedicalRecordDto>> GetMedicalRecord(Guid id)
			=> Ok(await serviceManager.PatientService().GetMedicalRecordByIdAsync(id));
		#endregion

		#region Lap Test End Points

		[HttpGet("AllLapTests/{recordId}")]
		public async Task<ActionResult<IEnumerable<LapTestDto>>> GetLapTests(Guid recordId)
				=> Ok(await serviceManager.PatientService().GetAllLapTestsAsync(recordId));
		[HttpGet("LapTests/{id}")]
		public async Task<ActionResult<LapTestDto>> GetLapTest(Guid id)
			=> Ok(await serviceManager.PatientService().GetLapTestByIdAsync(id));
		#endregion

		#region Radiation End Points
		[HttpGet("AllRadiations/{recordId}")]
		public async Task<ActionResult<IEnumerable<RadiologyDto>>> GetRadiations(Guid recordId)
				=> Ok(await serviceManager.PatientService().GetAllRadiationsAsync(recordId));
		[HttpGet("Radiations/{id}")]
		public async Task<ActionResult<RadiologyDto>> GetRadiation(Guid id)
			=> Ok(await serviceManager.PatientService().GetRadiologyByIdAsync(id));
		#endregion

		#region Patient End Points
		[HttpGet("Patient/{id}")]
		public async Task<ActionResult<PatientDto>> GetPatient(int id)
			=> Ok(await serviceManager.PatientService().GetPatientByIdAsync(id));
		#endregion

		#region Appointment End Points

		#endregion


	}
}
