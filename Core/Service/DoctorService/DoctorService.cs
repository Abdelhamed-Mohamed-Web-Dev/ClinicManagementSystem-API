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

        public async Task<IEnumerable<AppointmentDto1>> GetAllCanceledAppointment()
        {
            var AllAppoinment = await unitOfWork.GetRepository<Appointment,Guid>().GetAllAsync();
            var CancelAppointment = AllAppoinment.Where(a => a.Status==Domain.Entities.AppointmentStatus.Canceled);
            var CaneclAppointmentDto = mapper.Map<IEnumerable<AppointmentDto1>>(CancelAppointment);
            return CaneclAppointmentDto;
        }

        public async Task<IEnumerable<AppointmentDto1>> GetAllConfirmAppointment()
        {
            var AllAppoinment = await unitOfWork.GetRepository<Appointment, Guid>().GetAllAsync();
            var ConfirmAppointment = AllAppoinment.Where(a => a.Status == Domain.Entities.AppointmentStatus.Confirmed);
            var ConfirmAppointmentDto = mapper.Map<IEnumerable<AppointmentDto1>>(ConfirmAppointment);
            return ConfirmAppointmentDto ;

        }

        public async Task<IEnumerable<LapTestDto1>> GetAllLapTestOfPatientAysnc(Guid id)
        {
         var alllaptests= await unitOfWork.GetRepository<LapTest,Guid>().GetAllAsync();
            var laptests= alllaptests.Where(a=>a.MedicalId==id);
            var laptestsDto =mapper.Map<IEnumerable<Shared.DoctorModels.LapTestDto1>>(laptests);
            return laptestsDto;
        }

        public async Task<IEnumerable<AppointmentDto1>> GetAllPendingAppointment()
        {
            var AllAppoinment = await unitOfWork.GetRepository<Appointment, Guid>().GetAllAsync();
            var PendingAppointment = AllAppoinment.Where(a => a.Status == Domain.Entities.AppointmentStatus.Pending);
            var PendingAppointmentDto = mapper.Map<IEnumerable<AppointmentDto1>>(PendingAppointment);
            return PendingAppointmentDto;

        }

        public async Task<IEnumerable<RadiologyDto1>> GetAllRadiologyOfPatientAysnc(Guid id)
        {
            var allradiology = await unitOfWork.GetRepository<Radiology,Guid>().GetAllAsync();
            var radiologes = allradiology.Where(a=>a.Id==id);
            var radiologesDto=mapper.Map<IEnumerable<Shared.DoctorModels.RadiologyDto1>>(radiologes);
            return radiologesDto;
        }

        public async Task<PatientDto1> GetAppoitmentOfPatient(PatientDto1 patientDto1)
        {
            var PatientAppointment = await unitOfWork.GetRepository<Patient, int>().GetAsync(patientDto1.Id);
            var PatientAppointmentDto= mapper.Map<PatientDto1>(PatientAppointment);
            return PatientAppointmentDto;
        }

        public async Task<DoctorDto1> GetDoctorByIdAysnc(int id)
        {
            var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(id);
            var doctorDto=mapper.Map<Shared.DoctorModels.DoctorDto1>(doctor);
            return   doctorDto;
        }

        public async Task<PatientDto1> GetPatientByIdAysnc(PatientDto1 patientDto)
        {
            var patient= await unitOfWork.GetRepository<Patient,int>().GetAsync(patientDto.Id);
            var _patientDto = mapper.Map<Shared.DoctorModels.PatientDto1>(patient);
            return _patientDto;
        }

        
        public Task<DoctorDto1> UpdateDoctorByIdAysnc(DoctorDto1 doctorDto)
        {
            throw new NotImplementedException();
        }
    }
}
// بيانات حجز المريض 
// جميع الحجوزات ال Cofirm
// جميع الحجوزات ال Pending
// جميع الحجوزات ال Canceld
