using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
    public class LapTestNotFoundException(Guid id)
        : NotFoundException($"Lap Test With Id {id} Is NOT FOUND.")
    {
    }
}
