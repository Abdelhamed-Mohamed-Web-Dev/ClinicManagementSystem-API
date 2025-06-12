using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.Doctor
{
    public class MedicalRecordWithRadiologyAndLapTest : Specifications<MedicalRecord>
    {
        public MedicalRecordWithRadiologyAndLapTest(int PatientId, int DoctorId)
            : base(medicalrecord => medicalrecord.PatientId == PatientId && medicalrecord.DoctorId == DoctorId)
        {
            AddInclude(medicalrecord => medicalrecord.Radiation);
            AddInclude(medicalrecord => medicalrecord.LapTests);
            AddInclude(medicalrecord => medicalrecord.Patient);
            AddInclude(medicalrecord => medicalrecord.Doctor);


        }

        public MedicalRecordWithRadiologyAndLapTest()
            : base(null)
        {
            AddInclude(medicalrecord => medicalrecord.Radiation);
            AddInclude(medicalrecord => medicalrecord.LapTests);
            AddInclude(medicalrecord => medicalrecord.Patient);
            AddInclude(medicalrecord => medicalrecord.Doctor);

        }
    }
}
