using Service.Abstraction.DoctorService;
using Shared.DoctorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DoctorService
{
    public class DoctorService(IUnitOfWork unitOfWork, IMapper mapper) : IDoctorService
    {
        
        public async Task<IEnumerable<AppointmentDto1>> GetAllAppointmentOfDoctorAysnc(DoctorDto doctorDto)
        {
            var allappointment = await unitOfWork.GetRepository<Appointment, Guid>().GetAllAsync();
            var appointment = allappointment.Where(a => a.DoctorId == doctorDto.Id);
            var appointmentDto = mapper.Map<IEnumerable<Shared.DoctorModels.AppointmentDto1>>(appointment);
            return appointmentDto;
        }

        public async Task<IEnumerable<LapTestDto1>> GetAllLapTestOfPatientAysnc(Guid id)
        {
         var alllaptests= await unitOfWork.GetRepository<LapTest,Guid>().GetAllAsync();
            var laptests= alllaptests.Where(a=>a.MedicalId==id);
            var laptestsDto =mapper.Map<IEnumerable<Shared.DoctorModels.LapTestDto1>>(laptests);
            return laptestsDto;
        }

        public async Task<IEnumerable<RadiologyDto1>> GetAllRadiologyOfPatientAysnc(Guid id)
        {
            var allradiology = await unitOfWork.GetRepository<Radiology,Guid>().GetAllAsync();
            var radiologes = allradiology.Where(a=>a.Id==id);
            var radiologesDto=mapper.Map<IEnumerable<Shared.DoctorModels.RadiologyDto1>>(radiologes);
            return radiologesDto;
        }

        public async Task<DoctorDto1> GetDoctorByIdAysnc(int id)
        {
            var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(id);
            var doctorDto=mapper.Map<Shared.DoctorModels.DoctorDto1>(doctor);
            return   doctorDto;
        }

        public async Task<PatientDto1> GetPatientByIdAysnc(PatientDto patientDto)
        {
            var patient= await unitOfWork.GetRepository<Patient,int>().GetAsync(patientDto.Id);
            var _patientDto = mapper.Map<Shared.DoctorModels.PatientDto1>(patient);
            return _patientDto;
        }

        
        
        public Task<DoctorDto1> UpdateDoctorByIdAysnc(DoctorDto doctorDto)
        {
            throw new NotImplementedException();
        }
    }
}
