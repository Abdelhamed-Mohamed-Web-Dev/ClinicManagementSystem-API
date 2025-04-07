global using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Shared.PatientModels;

namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class PatientController(IServiceManager serviceManager) : ControllerBase
	{
		[HttpGet("Doctors")]
		public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
			=> Ok(await serviceManager.PatientService().GetAllDoctorsAsync());
		[HttpGet("Doctors/{id}")]
		public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
			=> Ok(await serviceManager.PatientService().GetDoctorByIdAsync(id));
		[HttpGet("AllMedicalRecords/{patientId}")]
		public async Task<ActionResult<IEnumerable<MedicalRecordDto>>> GetMedicalRecords(int patientId)
			=> Ok(await serviceManager.PatientService().GetAllMedicalRecordsAsync(patientId));
		[HttpGet("MedicalRecords/{id}")]
		public async Task<ActionResult<MedicalRecordDto>> GetMedicalRecord(Guid id)
			=> Ok(await serviceManager.PatientService().GetMedicalRecordByIdAsync(id));
		[HttpGet("AllLapTests/{recordId}")]
		public async Task<ActionResult<IEnumerable<LapTestDto>>> GetLapTests(Guid recordId)
				=> Ok(await serviceManager.PatientService().GetAllLapTestsAsync(recordId));
		[HttpGet("LapTests/{id}")]
		public async Task<ActionResult<LapTestDto>> GetLapTest(Guid id)
			=> Ok(await serviceManager.PatientService().GetLapTestByIdAsync(id));
		[HttpGet("AllRadiations/{recordId}")]
		public async Task<ActionResult<IEnumerable<RadiologyDto>>> GetRadiations(Guid recordId)
				=> Ok(await serviceManager.PatientService().GetAllRadiationsAsync(recordId));
		[HttpGet("Radiations/{id}")]
		public async Task<ActionResult<RadiologyDto>> GetRadiation(Guid id)
			=> Ok(await serviceManager.PatientService().GetRadiologyByIdAsync(id));
		[HttpGet("Patient/{id}")]
		public async Task<ActionResult<PatientDto>> GetPatient(int id)
			=> Ok(await serviceManager.PatientService().GetPatientByIdAsync(id));



	}
}
