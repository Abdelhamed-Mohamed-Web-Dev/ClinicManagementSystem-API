
namespace Persistence.Data.Configurations
{
    internal class MedicalRecordConfigurations : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasMany(m => m.LapTests)
                .WithOne(m => m.MedicalRecord)
                .HasForeignKey(m => m.MedicalId);
            builder.HasMany(m => m.Radiation)
                .WithOne(m => m.MedicalRecord)
                .HasForeignKey(m => m.MedicalRecordId);
        }
    }
}
