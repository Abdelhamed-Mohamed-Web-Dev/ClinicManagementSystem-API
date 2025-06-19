
using Shared.AdminModels;
using Shared.AppointmentModels;

namespace Presentation
{
	public class PatientController(IServiceManager serviceManager) : APIController
	{
		#region Doctor End Points
		[HttpGet("Doctors")]
		public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors([FromQuery] string? specialty, string? search)
			=> Ok(await serviceManager.PatientService().GetAllDoctorsAsync(specialty, search));
		[HttpGet("Doctor/id")]
		public async Task<ActionResult<DoctorDto>> GetDoctor([FromQuery] int id)
			=> Ok(await serviceManager.PatientService().GetDoctorByIdAsync(id));
		[HttpGet("Doctor/userName")]
		public async Task<ActionResult<DoctorDto>> GetDoctor([FromQuery] string userName)
			=> Ok(await serviceManager.PatientService().GetDoctorByUserNameAsync(userName));

		#endregion

		#region Medical Record End Points

		[HttpGet("MedicalRecords")]
		public async Task<ActionResult<IEnumerable<MedicalRecordDto>>> GetMedicalRecords([FromQuery] int patientId)
			=> Ok(await serviceManager.PatientService().GetAllMedicalRecordsAsync(patientId));
		[HttpGet("MedicalRecord")]
		public async Task<ActionResult<MedicalRecordDto>> GetMedicalRecord([FromQuery] Guid id)
			=> Ok(await serviceManager.PatientService().GetMedicalRecordByIdAsync(id));
		#endregion

		#region Lap Test End Points

		[HttpGet("LapTests")]
		public async Task<ActionResult<IEnumerable<LapTestDto>>> GetLapTests([FromQuery] Guid recordId)
				=> Ok(await serviceManager.PatientService().GetAllLapTestsAsync(recordId));
		[HttpGet("LapTest")]
		public async Task<ActionResult<LapTestDto>> GetLapTest([FromQuery] Guid id)
			=> Ok(await serviceManager.PatientService().GetLapTestByIdAsync(id));
		#endregion

		#region Radiation End Points
		[HttpGet("Radiations")]
		public async Task<ActionResult<IEnumerable<RadiologyDto>>> GetRadiations([FromQuery] Guid recordId)
				=> Ok(await serviceManager.PatientService().GetAllRadiationsAsync(recordId));
		[HttpGet("Radiation")]
		public async Task<ActionResult<RadiologyDto>> GetRadiation([FromQuery] Guid id)
			=> Ok(await serviceManager.PatientService().GetRadiologyByIdAsync(id));
		#endregion

		#region Patient End Points
		[HttpGet("Patient/id")]
		public async Task<ActionResult<PatientDto>> GetPatient([FromQuery] int id)
			=> Ok(await serviceManager.PatientService().GetPatientByIdAsync(id));

		[HttpGet("Patient/userName")]
		public async Task<ActionResult<PatientDto>> GetPatient([FromQuery] string userName)
			=> Ok(await serviceManager.PatientService().GetPatientByUserNameAsync(userName));

		[HttpPost("AddPatient")]
		public async Task<ActionResult<UserPatientDto>> AddPatient(UserPatientDto patient)
			=> Ok(await serviceManager.AdminService().AddPatientAsync(patient));

		[HttpPut("UpdatePatient")]
		public async Task<ActionResult<PatientDto>> UpdatePatient(UpdatePatientDto patient)
			=> Ok(await serviceManager.PatientService().UpdatePatientAsync(patient));

		#endregion

		#region Appointment End Points

		#region Available Date & Time End Points

		[HttpGet("AvailableDays")]
		public async Task<ActionResult<IEnumerable<AvailableDaysDto>>> GetAvailableDays([FromQuery] int doctorId)
		=> Ok(await serviceManager.PatientService().GetAllAvailableDaysAsync(doctorId));
		[HttpGet("AvailableTimes")]
		public async Task<ActionResult<IEnumerable<AvailableDaysDto>>> GetAvailableDays([FromQuery] int doctorId, DateTime date)
		=> Ok(await serviceManager.PatientService().GetAllAvailableTimesAsync(doctorId, date));

		#endregion

		[HttpGet("Appointments")]
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAllAppointments([FromQuery] int patientId)
		=> Ok(await serviceManager.PatientService().GetAllAppointmentsAsync(patientId));
		[HttpGet("Appointment")]
		public async Task<ActionResult<AppointmentDto>> GetAppointment([FromQuery] Guid id)
		=> Ok(await serviceManager.PatientService().GetAppointmentByIdAsync(id));
		[HttpPost("CreateAppointment")]
		public async Task<ActionResult<AppointmentDto>> CreateAppointment(CreateAppointmentDto appointment)
		=> Ok(await serviceManager.PatientService().CreateAppointmentAsync(appointment));
		[HttpPut("UpdateAppointment")]
		public async Task<ActionResult<AppointmentDto>> UpdateAppointment(UpdateAppointmentDto appointment)
		=> Ok(await serviceManager.PatientService().UpdateAppointmentAsync(appointment));
		[HttpPut("CancelAppointment")]
		public async Task<ActionResult<AppointmentDto>> CancelAppointment([FromQuery] Guid id)
		=> Ok(await serviceManager.PatientService().CancelAppointmentAsync(id));

		#endregion

		#region FavDoctors

		[HttpPost("AddFavoriteDoctor")]
		public async Task<IActionResult> AddFavDoctor(int DoctorId, int PatientId)
			=> Ok(await serviceManager.PatientService().AddFavoriteDoctorAsync(DoctorId, PatientId));

		[HttpDelete("RemoveDoctorFromFavorites")]
		public async Task<IActionResult> RemoveDoctorFromFavorites(int DoctorId, int PatientId)
			=> Ok(await serviceManager.PatientService().RemoveFavoriteDoctorAsync(DoctorId, PatientId));

		[HttpGet("FavoriteDoctors")]
		public async Task<IActionResult> GetAllFavDoctors(int patientId)
			=> Ok(await serviceManager.PatientService().GetAllFavoriteDoctorsAsync(patientId));

		#endregion

		#region Notifications
		[HttpPut("RateDoctor")]
		public async Task<ActionResult<bool>> RateDoctor([FromQuery] int doctorId, int rate)
			=> Ok(await serviceManager.PatientService().RateDoctorAsync(doctorId, rate));
		[HttpGet("Notifications")]
		public async Task<IActionResult> GetAllNotifications([FromQuery] int patientId)
			=> Ok(await serviceManager.NotificationService().GetAllNotifications(null,patientId: patientId));
		[HttpGet("Notification")]
		public async Task<IActionResult> GetNotification([FromQuery] int id)
			=> Ok(await serviceManager.NotificationService().GetNotification(id));
		[HttpPut("ReadNotification")]
		public async Task<IActionResult> ReadNotification([FromQuery] int id)
			=> Ok(await serviceManager.NotificationService().ReadNotification(id));
		[HttpDelete("DeleteNotification")]
		public async Task<IActionResult> DeleteNotification([FromQuery] int id)
			=> Ok(await serviceManager.NotificationService().DeleteNotification(id));

		#endregion
	}
}
