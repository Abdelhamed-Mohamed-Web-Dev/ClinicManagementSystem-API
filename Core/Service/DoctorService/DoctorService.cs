
using Shared.NotificationModels;

namespace Service.DoctorService
{
    public class DoctorService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : IDoctorService
	{

		#region Appointment
		public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentAysnc
		(int? doctorId, int? patientId, AppointmentStatus? status)
		{
			var appointments = await unitOfWork.GetRepository<Appointment, Guid>().
				GetAllAsync(new AppointmentSpecifications(doctorId, patientId, status));
			return mapper.Map<IEnumerable<AppointmentDto>>(appointments);
		}

		#endregion

		#region Medical Record
		public async Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecords()
		{
			var medicalrecord = await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAllAsync(new MedicalRecordWithRadiologyAndLapTest());
			var _medicalrecords = mapper.Map<IEnumerable<MedicalRecordDto>>(medicalrecord);
			return _medicalrecords;
		}

		public async Task<MedicalRecordDto> GetMedicalRecord(int PatientId, int DoctorId)
		{

			var medicalrecord = await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAsync(new MedicalRecordWithRadiologyAndLapTest(PatientId, DoctorId));
			var medicalrecordDto = mapper.Map<MedicalRecordDto>(medicalrecord);
			return medicalrecordDto;
		}


		#endregion

		#region Lap Test 

		public async Task<IEnumerable<LapTestDto1>> GetAllLapTestOfPatientAysnc(Guid id)
		{
			var MedicalRecord = await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAsync(id);
			//  var laptests= alllaptests.Where(a=>a.MedicalId==id);
			var LapTests = MedicalRecord.LapTests;
			var laptestsDto = mapper.Map<IEnumerable<LapTestDto1>>(LapTests);
			return laptestsDto;
		}

		#endregion

		#region Radiology 
		public async Task<IEnumerable<RadiologyDto1>> GetAllRadiologyOfPatientAysnc(Guid id)
		{
			var MedicalRecord = await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAsync(id);
			var Radiology = MedicalRecord.Radiation;
			var radiologesDto = mapper.Map<IEnumerable<RadiologyDto1>>(Radiology);
			return radiologesDto;
		}

		#endregion

		#region Doctor
		public async Task<DoctorDto1> GetDoctorByIdAsync(int id)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(id);
			var doctorDto = mapper.Map<Shared.DoctorModels.DoctorDto1>(doctor);
			return doctorDto;
		}
		public async Task<DoctorDto1> GetDoctorByUserNameAsync(string username)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAllAsync();
			var result = doctor.Where(d => d.UserName == username).FirstOrDefault();
			var doctorDto = mapper.Map<DoctorDto1>(result);
			return doctorDto;
		}
		public async Task<string> UpdateDoctorByIdAsync(UpdateDoctorDoctorDto _doctor)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(_doctor.Id);
			if (doctor == null) throw new DoctorNotFoundException(_doctor.Id);

			var doctorUser = await userManager.FindByEmailAsync(_doctor.OldEmail);
			if (doctorUser == null) throw new UserNotFoundException($"Email ({_doctor.OldEmail})");

			doctor.Name = _doctor.Name;
			doctor.Speciality = _doctor.Speciality;
			doctor.Phone = _doctor.Phone;
			doctor.Bio = _doctor.Bio;

			doctor.WorkingDays = _doctor.WorkingDays;
			doctor.StartTime = _doctor.StartTime;
			doctor.EndTime = _doctor.EndTime;
			doctor.AppointmentDuration = _doctor.AppointmentDuration;

			doctorUser.Email = _doctor.Email;
			doctorUser.PhoneNumber = _doctor.Phone;

			// Change password
			var result = await userManager.ChangePasswordAsync(doctorUser, _doctor.OldPassword, _doctor.NewPassword);

			if (!result.Succeeded) return "Can't Update Password";

			unitOfWork.GetRepository<Doctor, int>().Update(doctor);
			await unitOfWork.SaveChangesAsync();

			await userManager.UpdateAsync(doctorUser);

			return "Doctor Updated Successfully";
		}
		#endregion

		#region Patient
		public async Task<PatientDto1> GetPatientByIdAysnc(int id)
		{
			var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(id);
			var _patientDto = mapper.Map<PatientDto1>(patient);
			return _patientDto;
		}


		#endregion

		#region Notifications

		public async Task<IEnumerable<NotificationsDto>> GetAllNotifications(int doctorId)
		{
			var notifications = await unitOfWork.GetRepository<Notifications, int>().GetAllAsync(new NotificationSpecifications(doctorId, null));
			return mapper.Map<IEnumerable<NotificationsDto>>(notifications);
		}

		public async Task<NotificationsDto> GetNotification(int id)
		{
			var notification = await unitOfWork.GetRepository<Notifications, int>().GetAsync(id);
			return mapper.Map<NotificationsDto>(notification);
		}

		#endregion


	}
}
