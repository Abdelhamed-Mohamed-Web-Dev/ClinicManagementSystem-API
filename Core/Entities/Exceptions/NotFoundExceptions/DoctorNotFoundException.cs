namespace Domain.Exceptions.NotFoundExceptions
{
    public class DoctorNotFoundException(int id)
        : NotFoundException($"Doctor With Id {id} Is NOT FOUND.")
    {
    }
}
