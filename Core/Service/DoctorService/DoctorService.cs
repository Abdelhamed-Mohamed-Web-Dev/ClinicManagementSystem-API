using Service.Abstraction.DoctorService;
using Service.Specifications;
using Service.Specifications.Doctor;
using Shared.DoctorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Shared.AdminModels;

namespace Service.DoctorService
{
    public class DoctorService(IUnitOfWork unitOfWork, IMapper mapper) : IDoctorService
    {

        #region Appointment
        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentAysnc
        (int? doctorId, int? patientId, AppointmentStatus? status)
        {
            var appointments = await unitOfWork.GetRepository<Appointment, Guid>().
                GetAllAsync(new AppointmentSpecifications(doctorId, patientId, status));
            return mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        #endregion

        #region Medical Record
        public async Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecords(int doctorId)
            {
            var medicalrecord= await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAllAsync(new MedicalRecordWithRadiologyAndLapTest());
            medicalrecord = medicalrecord.Where(m => m.DoctorId == doctorId);
            var _medicalrecords = mapper.Map<IEnumerable<MedicalRecordDto>>(medicalrecord);
            return _medicalrecords;
            }

        public async Task<MedicalRecordDto> GetMedicalRecord(int PatientId, int DoctorId)
        {

            var medicalrecord = await unitOfWork.GetRepository<MedicalRecord, Guid>().GetAsync(new MedicalRecordWithRadiologyAndLapTest(PatientId, DoctorId));
            var medicalrecordDto = mapper.Map<MedicalRecordDto>(medicalrecord);
            return medicalrecordDto;
        }


        #endregion

        #region Lap Test 

        public async Task<IEnumerable<LapTestDto1>> GetAllLapTestOfPatientAysnc(Guid id)
        {
         var MedicalRecord= await unitOfWork.GetRepository<MedicalRecord,Guid>().GetAsync(id);
          //  var laptests= alllaptests.Where(a=>a.MedicalId==id);
          var LapTests=MedicalRecord.LapTests;
            var laptestsDto =mapper.Map<IEnumerable<LapTestDto1>>(LapTests);
            return laptestsDto;
        }

        #endregion

        #region Radiology 
        public async Task<IEnumerable<RadiologyDto1>> GetAllRadiologyOfPatientAysnc(Guid id)
        {
            var MedicalRecord = await unitOfWork.GetRepository<MedicalRecord,Guid>().GetAsync(id);
            var Radiology = MedicalRecord.Radiation;
            var radiologesDto=mapper.Map<IEnumerable<RadiologyDto1>>(Radiology);
            return radiologesDto;
        }

        #endregion

        #region Doctor
        public async Task<DoctorDto1> GetDoctorByIdAysnc(int id)
        {
            var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(id);
            var doctorDto=mapper.Map<Shared.DoctorModels.DoctorDto1>(doctor);
            return   doctorDto;
        }
        public async Task<DoctorDto1> GetDoctorByUserNameAysnc(string username)
        {
            var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAllAsync();
           var result = doctor.Where(d=>d.UserName==username).FirstOrDefault();
            var doctorDto=mapper.Map<DoctorDto1>(result);
            return   doctorDto;
        }
        public Task<DoctorDto1> UpdateDoctorByIdAysnc(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Patient
        public async Task<PatientDto1> GetPatientByIdAysnc(int id)
        {
            var patient= await unitOfWork.GetRepository<Patient,int>().GetAsync(id);
            var _patientDto = mapper.Map<PatientDto1>(patient);
            return _patientDto;
        }

        #endregion
   

      
      
    }
}
// بيانات حجز المريض 
// جميع الحجوزات ال Cofirm
// جميع الحجوزات ال Pending
// جميع الحجوزات ال Canceld
