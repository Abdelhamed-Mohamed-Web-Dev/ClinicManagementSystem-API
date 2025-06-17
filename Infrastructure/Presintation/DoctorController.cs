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
        [HttpGet("GetDoctorByID")]
        public async Task<IActionResult> GetDoctorById([FromQuery]int id) => Ok(await serviceManager.DoctorService().GetDoctorByIdAysnc(id));
        [HttpGet("GetDoctorByUserName")]
        public async Task<IActionResult> GetDoctorByUserName([FromQuery]string username) => Ok(await serviceManager.DoctorService().GetDoctorByUserNameAysnc(username));
        
        [HttpGet("Patient/{id}")]
        public async Task<IActionResult> GetPatient(int id) => Ok(await serviceManager.DoctorService().GetPatientByIdAysnc(id));

        [HttpGet("AllAppointment")]
        public async Task<IActionResult> GetAllAppointmet(int? doctorId, int? patientId, AppointmentStatus? status)
            => Ok(await serviceManager.DoctorService().GetAllAppointmentAysnc( doctorId, patientId,  status));

		[HttpGet("TodayAppointments")]
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetTodayAppointments([FromQuery] int doctorId, AppointmentStatus? status)
        => Ok(await serviceManager.AdminService().GetAppointmentsAsync(doctorId, null, DateTime.Now.Date, status));
        [HttpGet("UpcomingAppointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetUpcomingAppointments([FromQuery] int doctorId)
		=> Ok(await serviceManager.AdminService().GetUpcomingAppointmentsAsync(doctorId));

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
        [HttpGet ("AllMedicalRecords")]
        public async Task<IActionResult> GetAllMedicalRecords()=>Ok(await serviceManager.DoctorService().GetAllMedicalRecords());
        [HttpGet("MedicalRecordOfPatient")]
        public async Task<IActionResult> GetMedicalRecords(int PatientId,int DoctorId)=>Ok(await serviceManager.DoctorService().GetMedicalRecord(PatientId,DoctorId));
   
    }
}
