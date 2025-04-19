using Service.Abstraction;
using Shared.PatientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class DoctorController(IServiceManager serviceManager) : Controller
    {
        [HttpGet("Doctor/{id}")]
        public async Task<IActionResult> GetDoctor(int id) => Ok(await serviceManager.DoctorService().GetDoctorByIdAysnc(id));

        [HttpGet("Patient/{id}")]
        public async Task<IActionResult> GetPatient(PatientDto patientDto)
            => Ok(await serviceManager.DoctorService().GetPatientByIdAysnc(patientDto));
        [HttpGet("AllAppointment/{id}")]
        public async Task<IActionResult> GetAllAppointmet(DoctorDto doctorDto)
            => Ok(await serviceManager.DoctorService().GetAllAppointmentOfDoctorAysnc(doctorDto));
        [HttpGet("AllRadiology/{id}")]
        public async Task<IActionResult> GetAllRadioloy(Guid id)
            => Ok(await serviceManager.DoctorService().GetAllRadiologyOfPatientAysnc(id));
        [HttpGet("AllLapTests/{id}")]
        public async Task<IActionResult> GetAllLapTests(Guid id)
            => Ok(await serviceManager.DoctorService().GetAllLapTestOfPatientAysnc(id));
        [HttpGet]
        public async Task<IActionResult> UpdateDoctor(DoctorDto doctorDto) => Ok(await serviceManager.DoctorService().UpdateDoctorByIdAysnc(doctorDto));

    }
}
