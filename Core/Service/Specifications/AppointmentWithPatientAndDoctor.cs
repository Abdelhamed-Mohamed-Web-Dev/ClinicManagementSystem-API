using Domain.Exceptions;
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
            Includes(appointment => appointment.Doctor);
            Includes(appointment => appointment.Patient);

        }

        public AppointmentWithPatientAndDoctor() : base(null)
        {
            Includes(appointment => appointment.Doctor);
            Includes(appointment => appointment.Patient);

        }
    }
}
