namespace Service.Specifications.Patient
{
    internal class PatientSpecifications : BaseSpecifications<MedicalRecord>
    {
        // Get all => without criteria
        protected PatientSpecifications()
            : base(null)
        {
        }
    }
}
