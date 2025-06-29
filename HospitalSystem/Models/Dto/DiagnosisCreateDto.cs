namespace HospitalSystem.Models.Dto
{
    public class DiagnosisCreateDto
    {
        public string Description { get; set; }
        public string DescriptionDisease { get; set; }
        public string NameDisease { get; set; }
        public DateTime VisitDate { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
    }
}
