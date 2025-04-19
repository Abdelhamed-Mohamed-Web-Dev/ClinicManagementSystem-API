using Shared.DoctorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.DoctorService
{
    public interface IDoctorService
    {
     public Task<DoctorDto1> GetDoctorByIdAysnc(int id);
        //Get Info Of Doctor
        public Task<DoctorDto1> UpdateDoctorByIdAysnc(DoctorDto doctorDto);
        // Update Info Of Doctor
        public Task<IEnumerable<AppointmentDto1>> GetAllAppointmentOfDoctorAysnc(DoctorDto doctorDto);
        // Get All Appointmet Of Doctor
        public Task<IEnumerable<RadiologyDto1>> GetAllRadiologyOfPatientAysnc(Guid id);
        // Get All Radiology Of Patient
        public Task<IEnumerable<LapTestDto1>> GetAllLapTestOfPatientAysnc(Guid id);
        // Get All LapTest Of Patient
        public Task<PatientDto1> GetPatientByIdAysnc(PatientDto patientDto);
        // Get Info Of Patient


    }
}
// بيانات الدكتور 
// تعديل بيانات الدكتور 
// جميع المواعيد يتاعت الدكتور 
// جميه التحاليل بتاعت المريض 
// جميع الاشعة بتاعت المريض 
// بيانات الدكتور 
