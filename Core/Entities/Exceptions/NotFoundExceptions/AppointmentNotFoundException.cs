using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
    public class AppointmentNotFoundException(Guid id)
        : NotFoundException($"Appointment With Id {id} Is NOT FOUND.")
    {
    }
}
