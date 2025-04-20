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

    [ApiController]
    [Route("api/[controller]")]

    public class DoctorController(IServiceManager serviceManager) : Controller
    {
        [HttpGet("Doctor/{id}")]
        public async Task<IActionResult> GetDoctor(int id) => Ok(await serviceManager.DoctorService().GetDoctorByIdAysnc(id));

        [HttpGet("Patient/{id}")]
        public async Task<IActionResult> GetPatient(int id)
        
           
           => Ok(await serviceManager.DoctorService().GetPatientByIdAysnc(id));
        
        [HttpGet("AllAppointment/{id}")]
        public async Task<IActionResult> GetAllAppointmet(int id)
            => Ok(await serviceManager.DoctorService().GetAllAppointmentOfDoctorAysnc(id));
        [HttpGet("AllRadiology/{id}")]
        public async Task<IActionResult> GetAllRadioloy(Guid id)
            => Ok(await serviceManager.DoctorService().GetAllRadiologyOfPatientAysnc(id));
        [HttpGet("AllLapTests/{id}")]
        public async Task<IActionResult> GetAllLapTests(Guid id)
            => Ok(await serviceManager.DoctorService().GetAllLapTestOfPatientAysnc(id));
        [HttpGet ]
        public async Task<IActionResult> UpdateDoctor(int id) => Ok(await serviceManager.DoctorService().UpdateDoctorByIdAysnc(id));
        [HttpGet ("AllConfirmAppointment")]
        public async Task<IActionResult> GetALlConfirmAppointment()=>Ok( await serviceManager.DoctorService().GetAllConfirmAppointment());
        [HttpGet("AllCancelAppointment")]
        public async Task<IActionResult> GetAllCancelAppointment() => Ok(await serviceManager.DoctorService().GetAllCanceledAppointment());
        [HttpGet("AllPendingAppointment")]
        public async Task<IActionResult> GetALlPendingAppointment() => Ok(await serviceManager.DoctorService().GetAllPendingAppointment());
        [HttpGet("PaientAppointment")]
        public async Task<IActionResult> GetPaitentAppointment(int id) => Ok(await serviceManager.DoctorService().GetAppoitmentOfPatient(id));
    }
}
