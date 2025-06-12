using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
    public class RadiologyNotFoundException(Guid id)
        : NotFoundException($"Radiology With Id {id} Is NOT FOUND.")
    {
    }
}
