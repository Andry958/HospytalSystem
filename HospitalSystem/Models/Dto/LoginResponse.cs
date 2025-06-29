namespace HospitalSystem.Models.Dto
{
    public class LoginResponseDto
    {
        public Guid DoctorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
