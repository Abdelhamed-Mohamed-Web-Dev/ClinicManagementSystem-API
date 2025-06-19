
using Domain.Entities;
using Shared.AdminModels;
using Shared.AppointmentModels;
using Shared.NotificationModels;
using Shared.PatientModels;

namespace Service.Abstraction.AdminService
{
    public interface IAdminService
	{
		// Appointments
		public Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(int? doctorId, int? patientId, DateTime? date, Domain.Entities.AppointmentStatus? status);
		public Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(int? doctorId);
		public Task<AppointmentDto> ConfirmAppointmentAsync(Guid id);
		// Doctors
		public Task<UserDoctorDto> AddDoctorAsync(UserDoctorDto doctor);
		public Task<DoctorDto> UpdateDoctorAsync(UpdateDoctorDto _doctor);
		// Patients
		public Task<IEnumerable<PatientDto>> GetPatientsAsync(string? search);
		public Task<UserPatientDto> AddPatientAsync(UserPatientDto patient);


	}
}
