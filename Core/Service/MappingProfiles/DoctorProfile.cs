using Shared.DoctorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile() { 
        
        CreateMap<Doctor,DoctorDto1>().ReverseMap();
            CreateMap<Patient,PatientDto1>().ReverseMap();
            CreateMap<LapTest, LapTestDto1>().ReverseMap();
            CreateMap<Radiology, RadiologyDto1>().ReverseMap();
            CreateMap<Appointment, AppointmentDto1>()
                .ForMember(p1=>p1.PatientName,p2=>p2.MapFrom(p2=>p2.Patient.Name))
                .ForMember(p1=>p1.DoctorName,p2=>p2.MapFrom(p2=>p2.Doctor.Name))
                .ForMember(p1 =>p1.Status, p2 => p2.MapFrom(p2 =>p2.Status.ToString() ))
                .ForMember(p1 =>p1.Type, p2 => p2.MapFrom(p2 =>p2.Type.ToString() ))
                .ReverseMap();
            CreateMap<MedicalRecord,MedicalRecordDto1>()
                .ForMember(p1 => p1.PatientName, p2 => p2.MapFrom(p2 => p2.Patient.Name))
                .ForMember(p1 => p1.DoctorName, p2 => p2.MapFrom(p2 => p2.Doctor.Name))
                .ReverseMap();


        }
    }
}
