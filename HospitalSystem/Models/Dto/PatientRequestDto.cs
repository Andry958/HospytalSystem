namespace HospitalSystem.Models.Dto
{
    public class PatientRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
