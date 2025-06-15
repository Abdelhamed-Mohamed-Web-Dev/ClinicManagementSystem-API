using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.Patient
{
    public class PatientSpecifications : Specifications<Domain.Entities.Patient>
    {
        public PatientSpecifications(string userName) : base(p => p.UserName == userName)
        {
            AddInclude(p => p.Appointments);
            AddInclude(p => p.MedicalRecords);
        }
    }
}
