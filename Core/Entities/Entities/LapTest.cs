
namespace Domain.Entities
{
    public class LapTest : BaseEntity<Guid>
	{
        public string TestType { get; set; }
        public DateOnly TestDate { get; set; }
        public string TestResult { get; set; }
        public int TestUnits { get; set; }
        public string Comments { get; set; }
        public string LapName { get; set; }
        public int ReferenceRange { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public Guid MedicalId { get; set; }
        public byte[]? PdfFile { get; set; }
        public string? PdfFileName { get; set; }
    }
}
