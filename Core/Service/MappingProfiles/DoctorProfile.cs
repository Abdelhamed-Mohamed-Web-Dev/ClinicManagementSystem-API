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
                .ReverseMap();
            CreateMap<MedicalRecord,MedicalRecordDto1>()
                .ForMember(p1 => p1.PatientName, p2 => p2.MapFrom(p2 => p2.Patient.Name))
                .ReverseMap();


        }
    }
}
