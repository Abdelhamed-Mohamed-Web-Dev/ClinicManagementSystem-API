
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Shared.AdminModels;

namespace Service.AdminService
{
	public class AdminService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : IAdminService
	{
		// Appointment
		public async Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync
			(int? doctorId, int? patientId, DateTime? date, AppointmentStatus? status)
		{
			var appointments = await unitOfWork.GetRepository<Appointment, Guid>().
				GetAllAsync(new AppointmentSpecifications(doctorId, patientId, date, status));
			return mapper.Map<IEnumerable<AppointmentDto>>(appointments);
		}
		public async Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync()
		{
			var appointments = await unitOfWork.GetRepository<Appointment, Guid>().
				GetAllAsync(new AppointmentSpecifications(/*from tomorrow*/));
			return mapper.Map<IEnumerable<AppointmentDto>>(appointments);
		}
		// Doctor
		public async Task<UserDoctorDto> AddDoctorAsync(UserDoctorDto _doctor)
		{
			var user = new User()
			{
				Email = _doctor.Email,
				DisplayName = _doctor.DisplayName,
				UserName = _doctor.UserName,
				PhoneNumber = _doctor.PhoneNumber,
			};
			var result = await userManager.CreateAsync(user, _doctor.Password);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				throw new ValidationException(errors);
			}
			await userManager.AddToRoleAsync(user, "DOCTOR");

			var doctor = new Doctor()
			{
				Name = _doctor.Name,
				UserName = _doctor.UserName,
				Phone = _doctor.PhoneNumber,
				Speciality = _doctor.Speciality,
				PictureUrl = _doctor.PictureUrl,
				Rate = 5,
				NewVisitPrice = _doctor.NewVisitPrice,
				FollowUpVisitPrice = _doctor.FollowUpVisitPrice,
				WorkingDays = _doctor.WorkingDays,
				StartTime = _doctor.StartTime,
				EndTime = _doctor.EndTime,
				AppointmentDuration = _doctor.AppointmentDuration
			};
			await unitOfWork.GetRepository<Doctor, int>().AddAsync(doctor);
			await unitOfWork.SaveChangesAsync();
			return _doctor;
		}
		public async Task<DoctorDto> UpdateDoctorAsync(UpdateDoctorDto _doctor)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(_doctor.Id);
			if (doctor == null) throw new DoctorNotFoundException(_doctor.Id);

			doctor.Bio = _doctor.Bio;
			doctor.NewVisitPrice = _doctor.NewVisitPrice;
			doctor.FollowUpVisitPrice = _doctor.FollowUpVisitPrice;
			doctor.WorkingDays = _doctor.WorkingDays;
			doctor.StartTime = _doctor.StartTime;
			doctor.EndTime = _doctor.EndTime;
			doctor.AppointmentDuration = _doctor.AppointmentDuration;

			unitOfWork.GetRepository<Doctor, int>().Update(doctor);
			await unitOfWork.SaveChangesAsync();
			return mapper.Map<DoctorDto>(doctor);
		}
		// Patient
		public async Task<IEnumerable<PatientDto>> GetPatientsAsync(string? search)
		{
			var patients = await unitOfWork.GetRepository<Patient, int>().
				GetAllAsync(new PatientBySearchSpecifications(search));
			return mapper.Map<IEnumerable<PatientDto>>(patients);
		}
		public async Task<UserPatientDto> AddPatientAsync(UserPatientDto _patient)
		{
			var user = new User()
			{
				Email = _patient.Email,
				DisplayName = _patient.DisplayName,
				UserName = _patient.UserName,
				PhoneNumber = _patient.PhoneNumber,
			};
			var result = await userManager.CreateAsync(user, _patient.Password);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				throw new ValidationException(errors);
			}
			await userManager.AddToRoleAsync(user, "PATIENT");

			var patient = new Patient()
			{
				UserName = _patient.UserName,
				Name = _patient.Name,
				Address = _patient.Address,
				BirthDate = _patient.BirthDate,
				Phone = _patient.PhoneNumber,
				Gender = _patient.Gender,
			};
			await unitOfWork.GetRepository<Patient, int>().AddAsync(patient);
			await unitOfWork.SaveChangesAsync();
			return _patient;
		}
		
	}
}
