using Domain.Entities;
using Shared.AppointmentModels;
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
     public Task<DoctorDto1> GetDoctorByUserNameAysnc(string UserName);
        //Get Info Of Doctor
        public Task<DoctorDto1> UpdateDoctorByIdAysnc(int id);
        // Update Info Of Doctor
        public Task<IEnumerable<RadiologyDto1>> GetAllRadiologyOfPatientAysnc(Guid id);
        // Get All Radiology Of Patient
        public Task<IEnumerable<LapTestDto1>> GetAllLapTestOfPatientAysnc(Guid id);
        // Get All LapTest Of Patient
        public Task<PatientDto1> GetPatientByIdAysnc(int id);
        // Get Info Of Patient
        public Task<IEnumerable<AppointmentDto>> GetAllAppointmentAysnc(int? doctorId, int? patientId, Domain.Entities.AppointmentStatus? status);
       // public Task<IEnumerable<MedicalRecordDto1>> GetMedicalRecord(int id);
        public  Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecords();
        public  Task<MedicalRecordDto> GetMedicalRecord(int PatientId,int DoctorId);
    }
}
// بيانات الدكتور                 Done 
// تعديل بيانات الدكتور           Done 
// جميع المواعيد يتاعت الدكتور    Done 
// جميه التحاليل بتاعت المريض     Done 
// جميع الاشعة بتاعت المريض        Done 
// بيانات الدكتور                 Done 
////////////////////////////////////////////

// بيانات حجز المريض 
// جميع الحجوزات ال Cofirm
// جميع الحجوزات ال Pending
// جميع الحجوزات ال Canceld
