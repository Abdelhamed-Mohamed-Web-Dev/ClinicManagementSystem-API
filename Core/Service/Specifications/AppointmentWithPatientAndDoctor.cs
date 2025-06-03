using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class AppointmentWithPatientAndDoctor : Specifications<Appointment>
    {
        public AppointmentWithPatientAndDoctor(int PatientId,int DoctorId) : base(appointment=>appointment.PatientId==PatientId&&appointment.DoctorId==DoctorId)
        {
			AddInclude(appointment => appointment.Doctor);
			AddInclude(appointment => appointment.Patient);

        }

        public AppointmentWithPatientAndDoctor() : base(null)
        {
			AddInclude(appointment => appointment.Doctor);
			AddInclude(appointment => appointment.Patient);

        }
    }
}
