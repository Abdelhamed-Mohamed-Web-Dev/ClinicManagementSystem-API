
namespace Service.MappingProfiles
{
	public class PatientProfile : Profile
	{
		public PatientProfile()
		{
			CreateMap<Doctor, DoctorDto>().ReverseMap();
			CreateMap<Patient, PatientDto>().ReverseMap();
			CreateMap<LapTest, LapTestDto>().ReverseMap();
			CreateMap<Radiology, RadiologyDto>().ReverseMap();
			CreateMap<MedicalRecord, MedicalRecordDto>()
				.ForMember(d => d.DoctorName, opt => opt.MapFrom(s => s.Doctor.Name))
				.ForMember(d => d.PatientName, s => s.MapFrom(s => s.Patient.Name))
				;
			CreateMap<Appointment, AppointmentDto>()
				.ForMember(d => d.DoctorName, s => s.MapFrom(s => s.Doctor.Name))
				.ForMember(d => d.PatientName, s => s.MapFrom(s => s.Patient.Name))
				.ReverseMap();
			//
			CreateMap<Doctor_Rate, DoctorRateDto>()
			//	.ForMember(d => d.DoctorName, opt => opt.MapFrom(s => s.doctor.Name))
			//	.ForMember(d => d.PatientName, opt => opt.MapFrom(s => s.patient.Name))
				;
			//

        }
    }
}
