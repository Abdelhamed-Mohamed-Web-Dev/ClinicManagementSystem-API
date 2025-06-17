
using Domain.Contracts.IRepositories;
using Domain.Entities;
using Shared;
using Shared.AdminModels;

namespace Presentation
{
	public class AdminController(IServiceManager serviceManager) : APIController
	{

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
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAllAppointments([FromQuery] int? doctorId, int? patientId, DateTime? date, AppointmentStatus? status)
		=> Ok(await serviceManager.AdminService().GetAppointmentsAsync(doctorId, patientId, date, status));
		[HttpGet("TodayAppointments")]
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetTodayAppointments([FromQuery] int? doctorId, int? patientId, AppointmentStatus? status)
		=> Ok(await serviceManager.AdminService().GetAppointmentsAsync(doctorId, patientId, DateTime.Now.Date, status));
		[HttpGet("UpcomingAppointments")]
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetUpcomingAppointments([FromQuery] int? doctorId)
		=> Ok(await serviceManager.AdminService().GetUpcomingAppointmentsAsync(doctorId));
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
		[HttpPut("ConfirmAppointment")]
		public async Task<ActionResult<AppointmentDto>> ConfirmAppointment([FromQuery] Guid id)
		=> Ok(await serviceManager.AdminService().ConfirmAppointmentAsync(id));

		#endregion

		#region Doctor End Points
		[HttpGet("Doctors")]
		public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors([FromQuery] string? specialty, string? search)
			=> Ok(await serviceManager.PatientService().GetAllDoctorsAsync(specialty, search));
		[HttpGet("Doctor")]
		public async Task<ActionResult<DoctorDto>> GetDoctor([FromQuery] int id)
			=> Ok(await serviceManager.PatientService().GetDoctorByIdAsync(id));
		[HttpPost("AddDoctor")]
		public async Task<ActionResult<UserDoctorDto>> AddDoctor(UserDoctorDto doctor)
			=> Ok(await serviceManager.AdminService().AddDoctorAsync(doctor));
		[HttpPut("UpdateDoctor")]
		public async Task<ActionResult<UpdateDoctorDto>> UpdateDoctor(UpdateDoctorDto doctor)
			=> Ok(await serviceManager.AdminService().UpdateDoctorAsync(doctor));
		#endregion

		#region Patient End Points

		[HttpGet("Patient/id")]
		public async Task<ActionResult<PatientDto>> GetPatient([FromQuery] int id)
			=> Ok(await serviceManager.PatientService().GetPatientByIdAsync(id));

		[HttpGet("Patient/userName")]
		public async Task<ActionResult<PatientDto>> GetPatient([FromQuery] string userName)
			=> Ok(await serviceManager.PatientService().GetPatientByUserNameAsync(userName));

		[HttpGet("Patients")]
		public async Task<ActionResult<PatientDto>> GetPatients([FromQuery] string? search)
			=> Ok(await serviceManager.AdminService().GetPatientsAsync(search));

		[HttpPost("AddPatient")]
		public async Task<ActionResult<UserPatientDto>> AddPatient(UserPatientDto patient)
			=> Ok(await serviceManager.AdminService().AddPatientAsync(patient));

		[HttpPut("UpdatePatient")]
		public async Task<ActionResult<PatientDto>> UpdatePatient(UpdatePatientDto patient)
			=> Ok(await serviceManager.PatientService().UpdatePatientAsync(patient));


		#endregion

		#region Notifications
		[HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationsDto notification)
        {
            var result = await serviceManager.AdminService().SendNotificationAsync(notification);
			return Ok("Notification sent successfully.");
                
        }
		[HttpGet("GetNofiticationsOfPatient")]
		public async Task<IActionResult> GetNotificationsOfPatient(int PatientId) => Ok(await serviceManager.AdminService().GetAllNotificationsForPatientAsync(PatientId));
		[HttpGet("GetNofiticationsOfDoctor")]
		public async Task<IActionResult> GetNotificationsOfDoctor(int DoctorId) => Ok(await serviceManager.AdminService().GetAllNotificationsForDoctorAsync(DoctorId));
        #endregion

    }
}
