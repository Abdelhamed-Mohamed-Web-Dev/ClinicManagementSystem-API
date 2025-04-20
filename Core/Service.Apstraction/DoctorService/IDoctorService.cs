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
        public Task<DoctorDto1> UpdateDoctorByIdAysnc(int id);
        // Update Info Of Doctor
        public Task<IEnumerable<AppointmentDto1>> GetAllAppointmentOfDoctorAysnc(int id);
        // Get All Appointmet Of Doctor
        public Task<IEnumerable<RadiologyDto1>> GetAllRadiologyOfPatientAysnc(Guid id);
        // Get All Radiology Of Patient
        public Task<IEnumerable<LapTestDto1>> GetAllLapTestOfPatientAysnc(Guid id);
        // Get All LapTest Of Patient
        public Task<PatientDto1> GetPatientByIdAysnc(int id);
        // Get Info Of Patient
       public Task<PatientDto1> GetAppoitmentOfPatient(int id);
        // The Info About Appointmetn of Patient
        public Task< IEnumerable<AppointmentDto1>> GetAllConfirmAppointment();
        public Task< IEnumerable<AppointmentDto1>> GetAllPendingAppointment();
        public Task<IEnumerable<AppointmentDto1>> GetAllCanceledAppointment();


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
