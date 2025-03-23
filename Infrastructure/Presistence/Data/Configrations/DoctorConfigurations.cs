namespace Persistence.Data.Configurations
{
    internal class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasMany(d => d.Appointments)
                .WithOne(d => d.Doctor)
                .HasForeignKey(d => d.DoctorId);
        }
    }
}
