using Microsoft.AspNetCore.Authorization;
using Service.Abstraction;
using Shared.DoctorModels;
using Shared.PatientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{

    [Authorize(Roles = "Doctor")]
    public class DoctorController(IServiceManager serviceManager) : APIController
    {
        [HttpGet("Doctor/{id}")]
        public async Task<IActionResult> GetDoctor(int id) => Ok(await serviceManager.DoctorService().GetDoctorByIdAysnc(id));

        [HttpGet("Patient/{id}")]
        public async Task<IActionResult> GetPatient(int id) => Ok(await serviceManager.DoctorService().GetPatientByIdAysnc(id));
        
        
        //[HttpGet("MedicalRecord")]
        // public async Task<IActionResult> GetMedicalRecord(int id) => Ok(await serviceManager.DoctorService().GetMedicalRecord(id));
        [HttpGet("AllRadiology/{id}")]
        public async Task<IActionResult> GetAllRadioloy(Guid id)
            => Ok(await serviceManager.DoctorService().GetAllRadiologyOfPatientAysnc(id));
        [HttpGet("AllLapTests/{id}")]
        public async Task<IActionResult> GetAllLapTests(Guid id)
            => Ok(await serviceManager.DoctorService().GetAllLapTestOfPatientAysnc(id));
        [HttpGet ("UpdateDoctor") ]
        public async Task<IActionResult> UpdateDoctor(int id) => Ok(await serviceManager.DoctorService().UpdateDoctorByIdAysnc(id));
        [HttpGet("AllAppointment")]
        public async Task<IActionResult> GetAllCancelAppointment(int doctorId, int patientId, AppointmentStatus status) => Ok(await serviceManager.DoctorService().GetAllAppointmentAysnc(doctorId, patientId, status));
        [HttpGet ("AllMedicalRecords")]
        public async Task<IActionResult> GetAllMedicalRecords()=>Ok(await serviceManager.DoctorService().GetAllMedicalRecords());
        [HttpGet("MedicalRecordOfPatient")]
        public async Task<IActionResult> GetMedicalRecords(int PatientId,int DoctorId)=>Ok(await serviceManager.DoctorService().GetMedicalRecord(PatientId,DoctorId));
   
    }
}
