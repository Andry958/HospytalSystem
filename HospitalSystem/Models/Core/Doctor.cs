using System.Runtime.Intrinsics.X86;

namespace HospitalSystem.Models.Core
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
