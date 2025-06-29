namespace HospitalSystem.Models.Core
{
    public class Diagnosis
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Disease Disease { get; set; }
        public Guid DiseaseId { get; set; }
        public Visit Visit { get; set; }
        public Guid VisitId { get; set; }
    }
}
