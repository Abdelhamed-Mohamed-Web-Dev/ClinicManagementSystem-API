
namespace Persistence
{
	public class MainContext : DbContext
	{
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		=> modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);

		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<LapTest> LapTests { get; set; }
		public DbSet<MedicalRecord> MedicalRecords { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Radiology> Radiations { get; set; }

	}
}
