
namespace Presentation
{
	public class PatientController(IServiceManager serviceManager) : APIController
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
		[HttpGet("MedicalRecord/{id}")]
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

		#region Available Date & Time End Points

		[HttpGet("AvailableDays/{doctorId}")]
		public async Task<ActionResult<IEnumerable<AvailableDaysDto>>> GetAvailableDays(int doctorId)
		=> Ok(await serviceManager.PatientService().GetAllAvailableDaysAsync(doctorId));
		[HttpGet("AvailableTimes/{doctorId}+{date}")]
		public async Task<ActionResult<IEnumerable<AvailableDaysDto>>> GetAvailableDays(int doctorId, DateTime date)
		=> Ok(await serviceManager.PatientService().GetAllAvailableTimesAsync(doctorId, date));

		#endregion

		[HttpGet("AllAppointments/{patientId}")]
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAllAppointments(int patientId)
		=> Ok(await serviceManager.PatientService().GetAllAppointmentsAsync(patientId));
		[HttpGet("Appointment/{id}")]
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointment(Guid id)
		=> Ok(await serviceManager.PatientService().GetAppointmentByIdAsync(id));
		[HttpPost("CreateAppointment/{appointment}")]////////////////////////////////////////////
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> CreateAppointment(CreateAppointmentDto appointment)
		=> Ok();
		[HttpPut("UpdateAppointment/{id}")]//////////////////////////////////////////////////////
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> UpdateAppointment(Guid id)
		=> Ok();
		[HttpGet("CancelAppointment/{id}")]//////////////////////////////////////////////////////
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> CancelAppointment(Guid id)
		=> Ok();

        #endregion

        #region Rate
        ///

        [HttpPost("Rate_Doctor")]

		public async Task<IActionResult> RateDoctor(DoctorRateDto doctorRateDto)
		{
            try
            {
				// var result = await _patientService.RateDoctorAsync(dto);
				var result = await serviceManager.PatientService().PutRateAsync(doctorRateDto);
				return Ok(new { message = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }

		[HttpGet("Get Rate")]
		public async Task<IActionResult> GetRate(int DoctorId)=>Ok(await serviceManager.PatientService().GetDoctorRateAsync(DoctorId));

        ///

        #endregion
    }
}
