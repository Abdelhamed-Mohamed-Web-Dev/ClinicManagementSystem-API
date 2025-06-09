
using Shared.AppointmentModels;

namespace Service.Abstraction.PatientService
{
    public interface IPatientService
	{
		public Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync(string? specialty, string? search);
		public Task<DoctorDto> GetDoctorByIdAsync(int id);
		// Retrieve all records for patient by patientId
		public Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync(int patientId);
		// Retrieve record by id
		public Task<MedicalRecordDto> GetMedicalRecordByIdAsync(Guid id);
		// Retrieve all tests for one record by record id
		public Task<IEnumerable<LapTestDto>> GetAllLapTestsAsync(Guid medicalRecordId);
		// Retrieve test by id
		public Task<LapTestDto> GetLapTestByIdAsync(Guid id);
		// Retrieve all radiations for one record by record id
		public Task<IEnumerable<RadiologyDto>> GetAllRadiationsAsync(Guid medicalRecordId);
		// Retrieve radiology by id
		public Task<RadiologyDto> GetRadiologyByIdAsync(Guid id);
		// Retrieve patient data by id
		public Task<PatientDto> GetPatientByIdAsync(int id);
		// Retrieve all appointments by patient id
		public Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(int patientId);
		// Retrieve all appointment id
		public Task<AppointmentDto> GetAppointmentByIdAsync(Guid id);
		// Create appointment
		public Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointment);
		// Update appointment
		public Task<AppointmentDto> UpdateAppointmentAsync(UpdateAppointmentDto appointment);
		// Cancel appointment
		public Task<AppointmentDto> CancelAppointmentAsync(Guid id);
		// Retrieve available days
		public Task<IEnumerable<AvailableDaysDto>> GetAllAvailableDaysAsync(int doctorId);
		// Retrieve available times
		public Task<IEnumerable<AvailableTimesDto>> GetAllAvailableTimesAsync(int doctorId,DateTime date);
	
	}
}
