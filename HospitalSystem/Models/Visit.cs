namespace HospitalSystem.Models
{
    public class Visit
    {
        public Guid Id { get; set; }
        public DateTime VisitDate { get; set; }
        public Doctor Doctor { get; set; }
        public Guid DoctorId { get; set; }
        public Patient Patient { get; set; }
    }
}
