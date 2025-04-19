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
        
        CreateMap<Doctor,Shared.DoctorModels.DoctorDto1>().ReverseMap();
            CreateMap<Patient,Shared.DoctorModels.PatientDto1>().ReverseMap();
            CreateMap<LapTest, Shared.DoctorModels.LapTestDto1>().ReverseMap();
            CreateMap<Radiology, Shared.DoctorModels.RadiologyDto1>().ReverseMap();
            CreateMap<Appointment, Shared.DoctorModels.AppointmentDto1>()
                .ForMember(p1=>p1.PatientName,p2=>p2.MapFrom(p2=>p2.PatientId))
                .ReverseMap();
            CreateMap<MedicalRecord, Shared.DoctorModels.MedicalRecordDto1>()
                .ForMember(p1 => p1.PatientName, p2 => p2.MapFrom(p2 => p2.PatientId))
                .ReverseMap();


        }
    }
}
