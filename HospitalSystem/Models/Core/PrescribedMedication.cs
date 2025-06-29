namespace HospitalSystem.Models.Core
{
    public class PrescribedMedication
    {
        public Guid Id { get; set; }
        public Visit Visit { get; set; }
        public Guid VisitId { get; set; }
        public Medication Medication { get; set; }
        public Guid MedicationId { get; set; }
        public int Dosage { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
