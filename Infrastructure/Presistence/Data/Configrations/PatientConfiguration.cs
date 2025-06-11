
namespace Persistence.Data.Configurations
{
    internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasMany(p => p.Appointments)
                .WithOne(p=>p.Patient)
                .HasForeignKey(p=>p.PatientId);
            builder.HasMany(p=>p.MedicalRecords)
                .WithOne(p=>p.Patient)
                .HasForeignKey(p=>p.PatientId);
            builder.HasMany(p => p.rates)
                .WithOne(p => p.patient)
                .HasForeignKey(p => p.PatientId);
        }
    }
}
