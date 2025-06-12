using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
    public class MedicalRecordNotFoundException(Guid id)
        : NotFoundException($"Medical Record With Id {id} Is NOT FOUND.")
    {
    }
}
