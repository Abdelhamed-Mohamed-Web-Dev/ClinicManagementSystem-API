namespace Domain.Exceptions.NotFoundExceptions
{
    public class PatientNotFoundException(int id)
        : NotFoundException($"Patient With Id {id} Is NOT FOUND.")
    {
    }
}
