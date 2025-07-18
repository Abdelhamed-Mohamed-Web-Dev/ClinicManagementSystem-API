﻿using Microsoft.AspNetCore.Authorization;
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
        
        [HttpGet("AllAppointment/{id}")]
        public async Task<IActionResult> GetAllAppointmet(int id)
            => Ok(await serviceManager.DoctorService().GetAllAppointmentOfDoctorAysnc(id));
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
        [HttpGet ("AllConfirmAppointment")]
        public async Task<IActionResult> GetALlConfirmAppointment()=>Ok( await serviceManager.DoctorService().GetAllConfirmAppointment());
        [HttpGet("AllCancelAppointment")]
        public async Task<IActionResult> GetAllCancelAppointment() => Ok(await serviceManager.DoctorService().GetAllCanceledAppointment());
        [HttpGet("AllPendingAppointment")]
        public async Task<IActionResult> GetALlPendingAppointment() => Ok(await serviceManager.DoctorService().GetAllPendingAppointment());
        [HttpGet("PaientAppointment")]
        public async Task<IActionResult> GetPaitentAppointment(int id) => Ok(await serviceManager.DoctorService().GetAppoitmentOfPatient(id));
        [HttpGet ("AllMedicalRecords")]
        public async Task<IActionResult> GetAllMedicalRecords()=>Ok(await serviceManager.DoctorService().GetAllMedicalRecords());
        [HttpGet("MedicalRecordOfPatient")]
        public async Task<IActionResult> GetMedicalRecords(int PatientId,int DoctorId)=>Ok(await serviceManager.DoctorService().GetMedicalRecord(PatientId,DoctorId));
   
    }
}
