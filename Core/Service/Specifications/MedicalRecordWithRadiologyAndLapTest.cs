using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class MedicalRecordWithRadiologyAndLapTest : Specifications<MedicalRecord>
    {
        public MedicalRecordWithRadiologyAndLapTest(int PatientId,int DoctorId)
            : base(medicalrecord=>medicalrecord.PatientId==PatientId&&medicalrecord.DoctorId==DoctorId)
        {
            Includes(medicalrecord => medicalrecord.Radiation);
            Includes(medicalrecord => medicalrecord.LapTests);
            Includes(medicalrecord => medicalrecord.Patient);
            Includes(medicalrecord => medicalrecord.Doctor);


        }

        public MedicalRecordWithRadiologyAndLapTest()
            : base(null)
        {
            Includes(medicalrecord => medicalrecord.Radiation);
            Includes(medicalrecord => medicalrecord.LapTests);
            Includes(medicalrecord => medicalrecord.Patient);
            Includes(medicalrecord => medicalrecord.Doctor);

        }
    }
}
