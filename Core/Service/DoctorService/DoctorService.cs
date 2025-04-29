using Service.Abstraction.DoctorService;
using Service.Specifications;
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
        
        public async Task<IEnumerable<AppointmentDto1>> GetAllAppointmentOfDoctorAysnc(int id)
        {
            var allappointment = await unitOfWork.GetRepository<Appointment, Guid>().GetAllAsync();
            var appointment = allappointment.Where(a => a.DoctorId == id);
            var appointmentDto = mapper.Map<IEnumerable<Shared.DoctorModels.AppointmentDto1>>(appointment);
            return appointmentDto;
        }
        
        public async Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecords()
            {
            var medicalrecord= await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAllAsync(new MedicalRecordWithRadiologyAndLapTest());
            var _medicalrecords = mapper.Map<IEnumerable<MedicalRecordDto>>(medicalrecord);
            return _medicalrecords;
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
         var MedicalRecord= await unitOfWork.GetRepository<MedicalRecord,Guid>().GetAsync(id);
          //  var laptests= alllaptests.Where(a=>a.MedicalId==id);
          var LapTests=MedicalRecord.LapTests;
            var laptestsDto =mapper.Map<IEnumerable<LapTestDto1>>(LapTests);
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
            var MedicalRecord = await unitOfWork.GetRepository<MedicalRecord,Guid>().GetAsync(id);
            var Radiology = MedicalRecord.Radiation;
            var radiologesDto=mapper.Map<IEnumerable<RadiologyDto1>>(Radiology);
            return radiologesDto;
        }

        public async Task<PatientDto1> GetAppoitmentOfPatient(int id)
        {
            var PatientAppointment = await unitOfWork.GetRepository<Patient, int>().GetAsync(id);
            var PatientAppointmentDto= mapper.Map<PatientDto1>(PatientAppointment);
            return PatientAppointmentDto;
        }

        public async Task<DoctorDto1> GetDoctorByIdAysnc(int id)
        {
            var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(id);
            var doctorDto=mapper.Map<Shared.DoctorModels.DoctorDto1>(doctor);
            return   doctorDto;
        }

        
        public async Task<PatientDto1> GetPatientByIdAysnc(int id)
        {
            var patient= await unitOfWork.GetRepository<Patient,int>().GetAsync(id);
            var _patientDto = mapper.Map<PatientDto1>(patient);
            return _patientDto;
        }

        
        public Task<DoctorDto1> UpdateDoctorByIdAysnc(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<MedicalRecordDto> GetMedicalRecord(int PatientId,int DoctorId)
        {
            
            var medicalrecord = await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAsync(new MedicalRecordWithRadiologyAndLapTest(PatientId,DoctorId));
            var medicalrecordDto= mapper.Map<MedicalRecordDto>(medicalrecord);
            return medicalrecordDto;
        }
    }
}
// بيانات حجز المريض 
// جميع الحجوزات ال Cofirm
// جميع الحجوزات ال Pending
// جميع الحجوزات ال Canceld
