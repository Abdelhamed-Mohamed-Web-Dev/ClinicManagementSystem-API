

using Service.Specifications.Patient;

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

		// احنا كدا هنستهلك وقت كبير اوى الموضوع مش هيكون ملحوظ فى الاول
		// عشان الداتا لسه قليله بس لما الداتا تكبر هتظهر المشكله اننا بنجيب
		// الداتا كلها ونفلترها هنا عشان نطلع منها 2 او 3 ريكورد بالكتير 
		// ممكن عشان نحل المشكله نعمل Specification لكل entity 
		// بس دا هياخد وقت ف هنطنش دلوقتى لغاية م نبقا نفكر نكبر
		// الدنيا او لما المشكلة تظهر
		// ****** :) تم التعديل بنجاح ******
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
			var patient = await unitOfWork.GetRepository<Patient,int>().GetAsync(id);
			var patientDto = mapper.Map<PatientDto>(patient);
			return patientDto;
		}
	}
}
