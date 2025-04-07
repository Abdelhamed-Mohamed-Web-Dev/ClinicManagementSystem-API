using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			CreateMap<MedicalRecord,MedicalRecordDto>()
				.ForMember(d=>d.DoctorName,s=>s.MapFrom(s=>s.Doctor.Name))
				.ForMember(d=>d.PatientName,s=>s.MapFrom(s => s.Patient.Name))
				.ReverseMap();
			CreateMap<Appointment,AppointmentDto>()
				.ForMember(d=>d.DoctorName,s=>s.MapFrom(s=>s.Doctor.Name))
				.ForMember(d=>d.PatientName,s=>s.MapFrom(s => s.Patient.Name))
				.ReverseMap();
		}
	}
}
