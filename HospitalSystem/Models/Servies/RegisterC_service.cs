using HospitalSystem.Models.Core;
using HospitalSystem.Models.DB;
using HospitalSystem.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models.Services
{
    public class RegisterC_service
    {
        private readonly HospitalDbContext _context;

        public RegisterC_service(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, Doctor Message)> RegisterDoctorAsync(RegisterRequestDto request)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Login == request.Login);

            if (doctor != null)
            {
                return (false, doctor);
            }

            var newDoctor = new Doctor
            {
                Name = request.Name,
                LastName = request.LastName,
                Age = request.Age,
                Login = request.Login,
                Password = request.Password
            };

            await _context.Doctors.AddAsync(newDoctor);
            await _context.SaveChangesAsync();

            return (true, newDoctor);
        }
    }
}
