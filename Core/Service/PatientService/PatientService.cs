
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Service.PatientService
{
	public class PatientService(IUnitOfWork unitOfWork, IMapper mapper) : IPatientService
	{
		public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync(string? specialty, string? search)
		{

			var doctors = await unitOfWork.GetRepository<Doctor, int>().GetAllAsync(new DoctorSpecifications(specialty, search));
			var doctorsDto = mapper.Map<IEnumerable<DoctorDto>>(doctors);
			return doctorsDto;

		}
		public async Task<DoctorDto> GetDoctorByIdAsync(int id)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(id);
			var doctorDto = mapper.Map<DoctorDto>(doctor);
			return doctorDto;
		}

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
			var recordDto = mapper.Map<MedicalRecordDto>(record);
			return recordDto;
		}

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
			var testDto = mapper.Map<LapTestDto>(test);
			return testDto;
		}

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
			var radiologyDto = mapper.Map<RadiologyDto>(radiology);
			return radiologyDto;
		}

		public async Task<PatientDto> GetPatientByIdAsync(int id)
		{
			var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(id);
			var patientDto = mapper.Map<PatientDto>(patient);
			return patientDto;
		}

		public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(int patientId)
		{
			var appointments = await unitOfWork.GetRepository<Appointment, Guid>().GetAllAsync(new AppointmentSpecifications(patientId));
			return mapper.Map<IEnumerable<AppointmentDto>>(appointments);
		}

		public async Task<AppointmentDto> GetAppointmentByIdAsync(Guid id)
		{
			var appointment = await unitOfWork.GetRepository<Appointment, Guid>().GetAsync(new AppointmentSpecifications(id));
			return mapper.Map<AppointmentDto>(appointment);
		}

		public async Task<IEnumerable<AvailableDaysDto>> GetAllAvailableDaysAsync(int doctorId)
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(doctorId);
			var startDay = DateTime.Today; // available days from today
			var endDay = startDay.AddDays(28); // to month (28) days
			var result = new List<AvailableDaysDto>();
			var date = startDay.Date;

			while (date < endDay) {
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
		
		// exception handling required >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointment)//>>>>>>>>
		{
			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(new DoctorSpecifications(appointment.DoctorId));

			throw new NotImplementedException();

		}
		public Task<AppointmentDto> UpdateAppointmentAsync(AppointmentDto appointment)//>>>>>>>>
		{
			throw new NotImplementedException();

		}

        #region Rate &FavDoctors
        public async Task<string> PutRateAsync(DoctorRateDto doctorRateDto)
        {
            if (doctorRateDto.Rating < 1 || doctorRateDto.Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(doctorRateDto.DoctorId);
			var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(doctorRateDto.PatientId);

            if (doctor == null || patient == null)
                throw new ArgumentException("Doctor or patient not found.");
			var NewRate = new Doctor_Rate
			{
				PatientId = patient.Id,
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
			rates = rates.Where(r=>r.DoctorId==DoctorId).ToList();
			float result=0;
			int length=0;

			foreach (var rate in rates)
			{
				length++;
				result += rate.Rating;
			}
            return (result/length);
        }

        public async Task<string> AddFavoriteDoctorAsync(int DoctorId, int PatientId)
        {
			var doctor = await unitOfWork.GetRepository<Doctor,int>().GetAsync(DoctorId);
            var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(PatientId);

            if (doctor == null || patient == null)
               return ("Doctor or patient not found.");

            var found = await unitOfWork
            .GetRepository<FavoriteDoctors, int>()
            .AnyAsync(fv => fv.PatientId == PatientId && fv.DoctorId == DoctorId);
            if (found)
				return "This Doctor Already Added";

			await unitOfWork.GetRepository<FavoriteDoctors, int>().AddAsync(new FavoriteDoctors
			{
				DoctorId= DoctorId,
				PatientId= PatientId,
			});

			await unitOfWork.SaveChangesAsync();


            return "Add Doctor To Favorite";
        }

        public async Task<string> RemoveFavoriteDoctorAsync(int DoctorId, int PatientId)
        {
            var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(DoctorId);
            var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(PatientId);

            if (doctor == null || patient == null)
                return ("Doctor or patient not found.");

			var favorites = await unitOfWork.GetRepository<FavoriteDoctors, int>().GetAllAsync();
		var	result= favorites.FirstOrDefault(f=>f.PatientId==PatientId&&f.DoctorId==DoctorId);
			 unitOfWork.GetRepository<FavoriteDoctors, int>().Delete(result);
			await unitOfWork.SaveChangesAsync();
			return "Removeing Doctor From Favorites";
		}

  //      public async Task<DoctorDto> GetAllFavoriteDoctorsAsync( int PatientId)
  //      {
  //          var patient = await unitOfWork.GetRepository<Patient, int>().GetAsync(PatientId);
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

        #endregion
    }
}
