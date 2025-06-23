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
        public async Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(int? doctorId)
        {
            var appointments = await unitOfWork.GetRepository<Appointment, Guid>().
                GetAllAsync(new AppointmentSpecifications(doctorId, null, null, null));
            return mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
        public async Task<AppointmentDto> ConfirmAppointmentAsync(Guid id)
        {
            // Check if appointment is exist
            var appointment = await unitOfWork.GetRepository<Appointment, Guid>().GetAsync(id);
            if (appointment == null) throw new AppointmentNotFoundException(id);

            if (appointment.Status == AppointmentStatus.Canceled)
                return mapper.Map<AppointmentDto>(appointment);

            appointment.Status = AppointmentStatus.Confirmed;

            unitOfWork.GetRepository<Appointment, Guid>().Update(appointment);
            var medicalrecord = new MedicalRecord()
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Diagnoses="",
                Speciality=appointment.Doctor.Speciality,
                Prescription="",
                            };
            await unitOfWork.GetRepository<MedicalRecord,Guid>().AddAsync(medicalrecord);
            await unitOfWork.SaveChangesAsync();
            var notificationPatient = new Notifications()
            {
                PatientId = appointment.Patient.Id,
                Subject = "Rate Doctor",
                Body = $"Please click here to rate {appointment.Doctor.Name}. Thank you!",
                Type = NotificationType.RateDoctor,
                DoctorToRate = appointment.DoctorId
            };
            var notificationDoctor = new Notifications()
            {
                DoctorId = appointment.Doctor.Id,
                Subject = "Add Medical Record",
                Body = $"Please click here to write medical record fo patient ({appointment.Patient.Name}). Thank you!",
                Type = NotificationType.MedicalRecord,
            };
            await unitOfWork.GetRepository<Notifications, int>().AddAsync(notificationPatient);
            await unitOfWork.GetRepository<Notifications, int>().AddAsync(notificationDoctor);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<AppointmentDto>(appointment);
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
                RateList = new List<int>() { 5 },
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