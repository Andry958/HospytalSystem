using HospitalSystem.Models.DB;
using HospitalSystem.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models.Services
{
    public class LoginC_service
    {
        private readonly HospitalDbContext _context;

        public LoginC_service(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            {
                return null;
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d =>
                d.Login == request.Login && d.Password == request.Password);

            if (doctor == null)
            {
                return null;
            }

            return new LoginResponseDto
            {
                DoctorId = doctor.Id,
                Name = doctor.Name,
                LastName = doctor.LastName,
                Age = doctor.Age
            };
        }
    }
}
