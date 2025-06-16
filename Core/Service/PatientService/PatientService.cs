
using Shared.AdminModels;

namespace Service.PatientService
{
	public class PatientService(IUnitOfWork unitOfWork, IMapper mapper) : IPatientService
	{
		#region Doctor
		public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync(string? specialty, string? search)
		{

			var doctors = await unitOfWork.GetRepository<Doctor, int>().GetAllAsync(new DoctorSpecifications(specialty, search));
			var doctorsDto = mapper.Map<IEnumerable<DoctorDto>>(doctors);
			return doctorsDto;

		}
			public async Task<DoctorDto> GetDoctorByIdAsync(int id)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(id);
			return doctor == null
				? throw new DoctorNotFoundException(id)
				: mapper.Map<DoctorDto>(doctor);
		}
		public async Task<DoctorDto> GetDoctorByUserNameAsync(string userName)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(new DoctorSpecifications(userName));
			return doctor == null
				? throw new NotFoundException($"Doctor With UserName ({userName}) Isn't Found")
				: mapper.Map<DoctorDto>(doctor);
		}

		#endregion

		#region Medical Record

		public async Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync(int patientId)
		{
			var specifications = new MedicalRecordSpecifications(patientId);
			var allRecords = await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAllAsync(specifications);
			//var records = allRecords.Where(p => p.PatientId == patientId);
			var recordsDto = mapper.Map<IEnumerable<MedicalRecordDto>>(allRecords);
			return recordsDto;
		}
		public async Task<MedicalRecordDto> GetMedicalRecordByIdAsync(Guid id)
		{
			var specifications = new MedicalRecordSpecifications(id);
			var record = await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAsync(specifications);
			return record == null
				? throw new MedicalRecordNotFoundException(id)
				: mapper.Map<MedicalRecordDto>(record);
		}

		#endregion

		#region Lap Test
		public async Task<IEnumerable<LapTestDto>> GetAllLapTestsAsync(Guid medicalRecordId)
		{
			var allTests = await unitOfWork.GetRepository<LapTest, Guid>().GetAllAsync();
			var tests = allTests.Where(t => t.MedicalId == medicalRecordId);
			var testsDto = mapper.Map<IEnumerable<LapTestDto>>(tests);
			return testsDto;
		}
		public async Task<LapTestDto> GetLapTestByIdAsync(Guid id)
		{
			var test = await unitOfWork.GetRepository<LapTest, Guid>().GetAsync(id);
			return test == null
				? throw new LapTestNotFoundException(id)
				: mapper.Map<LapTestDto>(test);
		}

		#endregion
		
		#region Radiology

		public async Task<IEnumerable<RadiologyDto>> GetAllRadiationsAsync(Guid medicalRecordId)
		{
			var allRadiologies = await unitOfWork.GetRepository<Radiology, Guid>().GetAllAsync();
			var radiologies = allRadiologies.Where(t => t.MedicalRecordId == medicalRecordId);
			var radiologiesDto = mapper.Map<IEnumerable<RadiologyDto>>(radiologies);
			return radiologiesDto;
		}
		public async Task<RadiologyDto> GetRadiologyByIdAsync(Guid id)
		{
			var radiology = await unitOfWork.GetRepository<Radiology, Guid>().GetAsync(id);
			return radiology == null
				? throw new RadiologyNotFoundException(id)
				: mapper.Map<RadiologyDto>(radiology);
		}

		#endregion

		#region Patient

		public async Task<PatientDto> GetPatientByIdAsync(int id)
		{
			var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(id);
			return patient == null
				? throw new PatientNotFoundException(id)
				: mapper.Map<PatientDto>(patient);
		}
		public async Task<PatientDto> GetPatientByUserNameAsync(string userName)
		{
			var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(new PatientSpecifications(userName));
			return patient == null
				? throw new NotFoundException($"Patient With UserName ({userName}) Isn't Found")
				: mapper.Map<PatientDto>(patient);
		}
		public async Task<PatientDto> UpdatePatientAsync(UpdatePatientDto _patient)
		{
			var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(_patient.Id);
			if (patient == null) throw new PatientNotFoundException(_patient.Id);

			patient.BirthDate = _patient.BirthDate;
			patient.Name = _patient.Name;
			patient.Address = _patient.Address;

			unitOfWork.GetRepository<Patient, int>().Update(patient);
			await unitOfWork.SaveChangesAsync();
			return mapper.Map<PatientDto>(patient);
		}
		#endregion

		#region Appointment

		public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(int patientId)
		{
			// Check if the doctor is exist
			var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(patientId);
			if (patient == null) throw new PatientNotFoundException(patientId);

			var appointments = await unitOfWork.GetRepository<Appointment, Guid>().GetAllAsync(new AppointmentSpecifications(patientId));
			return mapper.Map<IEnumerable<AppointmentDto>>(appointments);
		}

		public async Task<AppointmentDto> GetAppointmentByIdAsync(Guid id)
		{
			var appointment = await unitOfWork.GetRepository<Appointment, Guid>().GetAsync(new AppointmentSpecifications(id));
			if (appointment == null) throw new AppointmentNotFoundException(id);

			return mapper.Map<AppointmentDto>(appointment);
		}

		public async Task<IEnumerable<AvailableDaysDto>> GetAllAvailableDaysAsync(int doctorId)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(doctorId);
			if (doctor == null) throw new DoctorNotFoundException(doctorId);
			var startDay = DateTime.Today; // available days from today
			var endDay = startDay.AddDays(28); // to month (28) days
			var result = new List<AvailableDaysDto>();
			var date = startDay.Date;

			while (date < endDay)
			{
				var dayOfWeek = (int)date.DayOfWeek;
				result.Add(new AvailableDaysDto
				{
					Date = date.Date,
					DayName = GetDayName(dayOfWeek),
					IsAvailable = doctor.WorkingDays.Contains(dayOfWeek)
				});
				date = date.AddDays(1);
			}

			return result;
		}
		private string GetDayName(int dayOfWeek)
		{
			return dayOfWeek switch
			{
				0 => "Sunday",
				1 => "Monday",
				2 => "Tuesday",
				3 => "Wednesday",
				4 => "Thursday",
				5 => "Friday",
				6 => "Saturday",
				_ => string.Empty
			};
		}
		public async Task<IEnumerable<AvailableTimesDto>> GetAllAvailableTimesAsync(int doctorId, DateTime date)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(new DoctorSpecifications(doctorId));
			if (doctor == null) throw new DoctorNotFoundException(doctorId);
			var result = new List<AvailableTimesDto>();
			var duration = TimeSpan.FromMinutes(doctor.AppointmentDuration);
			var startTime = date.Date.Add(doctor.StartTime);
			var endTime = date.Date.Add(doctor.EndTime);

			// check if date is in doctor's working days
			var day = (int)date.DayOfWeek;
			if (!doctor.WorkingDays.Contains(day)) return result; // return empty list

			// get booked appointments list
			var bookedAppointments = doctor.Appointments
			.Where(a => a.AppointmentDateTime.Date == date.Date && a.IsBooked).ToList();

			var currentTime = startTime;
			while (currentTime.Add(duration) <= endTime)
			{
				// check if booked list has current time or not
				// if it has isBooked = true otherwise = false 
				var isBooked = bookedAppointments.Any(a =>
					// any booked time ( current time <= booked time > current+duration)
					//				   (    9         <=   9 , 9:30	 >      9:30	   ) -> booked = true
					//				   (    6         <=      9		 >      6:30	   ) -> booked = false
					a.AppointmentDateTime >= currentTime &&
					a.AppointmentDateTime < currentTime.Add(duration));

				result.Add(new AvailableTimesDto
				{
					StartTime = currentTime.TimeOfDay,
					EndTime = currentTime.Add(duration).TimeOfDay,
					IsAvailable = !isBooked
				});

				currentTime = currentTime.Add(duration);
			}
			return result;
		}

		public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto _appointment)
		{
			// Check if the doctor is exist
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(new DoctorSpecifications(_appointment.DoctorId));
			if (doctor == null) throw new DoctorNotFoundException(_appointment.DoctorId);

			// Check if the doctor is exist
			var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(_appointment.PatientId);
			if (patient == null) throw new PatientNotFoundException(_appointment.PatientId);

			// Check if the date is a working day
			var day = (int)_appointment.AppointmentDateTime.DayOfWeek;
			if (!doctor.WorkingDays.Contains(day))
				throw new DateTimeIsNotAvailableException($"Date ({_appointment.AppointmentDateTime.DayOfWeek}) Is Outside Doctor's Working Days");

			// Check if time is within working hours
			var appointmentTime = _appointment.AppointmentDateTime.TimeOfDay;
			if (appointmentTime < doctor.StartTime || appointmentTime >= doctor.EndTime)
				throw new DateTimeIsNotAvailableException($"Time ({_appointment.AppointmentDateTime.TimeOfDay}) Is Outside Doctor's Working Hours");

			// Check if slot is available
			var existingAppointment = doctor.Appointments
				.FirstOrDefault(a => a.AppointmentDateTime == _appointment.AppointmentDateTime && a.IsBooked);
			if (existingAppointment != null)
				throw new DateTimeIsNotAvailableException($"Time Slot ({_appointment.AppointmentDateTime.TimeOfDay}) Is Already Booked");

			// Create new _appointment
			var newAppointment = new Appointment()
			{
				DoctorId = _appointment.DoctorId,
				PatientId = _appointment.PatientId,
				AppointmentDateTime = _appointment.AppointmentDateTime,
				Status = AppointmentStatus.Pending,
				Type = (AppointmentType)_appointment.Type,
				IsBooked = true,
			};

			// Add & Save changes
			await unitOfWork.GetRepository<Appointment, Guid>().AddAsync(newAppointment);
			await unitOfWork.SaveChangesAsync();

			// Return new _appointment
			return mapper.Map<AppointmentDto>(newAppointment);
		}
		public async Task<AppointmentDto> UpdateAppointmentAsync(UpdateAppointmentDto _appointment)
		{
			// Check if appointment is exist
			var appointment = await unitOfWork.GetRepository<Appointment, Guid>().GetAsync(new AppointmentSpecifications(_appointment.Id));
			if (appointment == null) throw new AppointmentNotFoundException(_appointment.Id);

			// Check if the _appointment is canceled | confirmed | at the past
			if (appointment.Status == AppointmentStatus.Canceled)
				throw new NotFoundException("Can't Update This Appointment (The Appointment Is Canceled)");
			if (appointment.Status == AppointmentStatus.Confirmed)
				throw new NotFoundException("Can't Update This Appointment (The Appointment Is Confirmed)");
			if (appointment.AppointmentDateTime <= DateTime.Now)
				throw new NotFoundException("Can't Update This Appointment (Its In The Past)");

			// Check if the doctor is exist
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(new DoctorSpecifications(appointment.DoctorId));
			if (doctor == null) throw new DoctorNotFoundException(appointment.DoctorId);

			// Check if the doctor is exist
			var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(appointment.PatientId);
			if (patient == null) throw new PatientNotFoundException(appointment.PatientId);

			// Check if the date is a working day
			var day = (int)_appointment.AppointmentDateTime.DayOfWeek;
			if (!doctor.WorkingDays.Contains(day))
				throw new DateTimeIsNotAvailableException($"Date ({_appointment.AppointmentDateTime.DayOfWeek}) Is Outside Doctor's Working Days");

			// Check if time is within working hours
			var appointmentTime = _appointment.AppointmentDateTime.TimeOfDay;
			if (appointmentTime < doctor.StartTime || appointmentTime >= doctor.EndTime)
				throw new DateTimeIsNotAvailableException($"Time ({_appointment.AppointmentDateTime.TimeOfDay}) Is Outside Doctor's Working Hours");

			// Check if slot is available
			var existingAppointment = doctor.Appointments
				.FirstOrDefault(a => a.AppointmentDateTime == _appointment.AppointmentDateTime && a.IsBooked);
			if (existingAppointment != null)
				throw new DateTimeIsNotAvailableException($"Time ({_appointment.AppointmentDateTime.TimeOfDay}) Is Already Booked");

			// Update & Save changes
			appointment.AppointmentDateTime = _appointment.AppointmentDateTime;
			unitOfWork.GetRepository<Appointment, Guid>().Update(appointment);
			await unitOfWork.SaveChangesAsync();

			// Return
			return mapper.Map<AppointmentDto>(appointment);

		}
		public async Task<AppointmentDto> CancelAppointmentAsync(Guid id)
		{
			// Check if appointment is exist
			var appointment = await unitOfWork.GetRepository<Appointment, Guid>().GetAsync(id);
			if (appointment == null) throw new AppointmentNotFoundException(id);

			if (appointment.Status == AppointmentStatus.Canceled)
				return mapper.Map<AppointmentDto>(appointment);

			var canceledAppointment = appointment;
			canceledAppointment.IsBooked = false;
			canceledAppointment.Status = AppointmentStatus.Canceled;

			unitOfWork.GetRepository<Appointment, Guid>().Update(canceledAppointment);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<AppointmentDto>(canceledAppointment);
		}

		#endregion

		/*
        public async Task<string> PutRateAsync(DoctorRateDto doctorRateDto)
        {
            if (doctorRateDto.Rating < 1 || doctorRateDto.Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(doctorRateDto.DoctorId);
            var doctor = await unitOfWork.GetRepository<Patient, int>().GetAsync(doctorRateDto.PatientId);

            if (doctor == null || doctor == null)
                throw new ArgumentException("Doctor or doctor not found.");
            var NewRate = new Doctor_Rate
            {
                PatientId = doctor.Id,
                DoctorId = doctor.Id,
                Rating = doctorRateDto.Rating
            };

            await unitOfWork.GetRepository<Doctor_Rate, int>().AddAsync(NewRate);
            await unitOfWork.SaveChangesAsync();

            return "Rating submitted successfully.";



        }

        public async Task<float> GetDoctorRateAsync(int DoctorId)
        {
            var rates = await unitOfWork.GetRepository<Doctor_Rate, int>().GetAllAsync();
            rates = rates.Where(r => r.DoctorId == DoctorId).ToList();
            float result = 0;
            int length = 0;

            foreach (var rate in rates)
            {
                length++;
                result += rate.Rating;
            }
            return (result / length);
        }
		*/
		public async Task<string> AddFavoriteDoctorAsync(int DoctorId, int PatientId)
        {
            var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(DoctorId);
            var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(PatientId);

            if (doctor == null || patient == null)
                return ("Doctor or doctor not found.");

            var found = await unitOfWork
            .GetRepository<FavoriteDoctors, int>()
            .AnyAsync(fv => fv.PatientId == PatientId && fv.DoctorId == DoctorId);
            if (found)
                return "This Doctor Already Added";

            await unitOfWork.GetRepository<FavoriteDoctors, int>().AddAsync(new FavoriteDoctors
            {
                DoctorId = DoctorId,
                PatientId = PatientId,
            });

            await unitOfWork.SaveChangesAsync();


            return "Add Doctor To Favorite";
        }

        public async Task<string> RemoveFavoriteDoctorAsync(int DoctorId, int PatientId)
        {
            var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(DoctorId);
            var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(PatientId);

            if (doctor == null || patient == null)
                return ("Doctor or doctor not found.");

            var favorites = await unitOfWork.GetRepository<FavoriteDoctors, int>().GetAllAsync();
            var result = favorites.FirstOrDefault(f => f.PatientId == PatientId && f.DoctorId == DoctorId);
			if (result == null)
				return ("Not Found");
            unitOfWork.GetRepository<FavoriteDoctors, int>().Delete(result);
            await unitOfWork.SaveChangesAsync();
            return "Removeing Doctor From Favorites";
        }


		//      public async Task<DoctorDto> GetAllFavoriteDoctorsAsync( int PatientId)
		//      {
		//          var doctor = await unitOfWork.GetRepository<Patient, int>().GetAsync(PatientId);
		//          var favorites = await unitOfWork.GetRepository<FavoriteDoctors, int>().GetAllAsync();
		//              favorites= favorites.Where(f=>f.PatientId==PatientId);
		//          var result = await unitOfWork.GetRepository<Doctor, int>().GetAllAsync();
		////	List<Doctor> doctors = new List<Doctor>();
		//          foreach (var fav in favorites)
		//	{
		//		result += result.Where(r => r.Id == fav.Id);
		//	}
		//          return mapper.Map<DoctorDto>(result);
		//}


	}
}
