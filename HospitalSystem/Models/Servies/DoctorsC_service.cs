using HospitalSystem.Models.Core;
using HospitalSystem.Models.DB;
using HospitalSystem.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models.Servies
{
    public class DoctorsC_service
    {
        private readonly HospitalDbContext _context;

        public DoctorsC_service(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }
        public async Task <Guid> Deleted(Guid doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
            if (doctor == null)
                throw new KeyNotFoundException("Doctor not found");

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return doctorId;
        }
        public async Task<Doctor?> UpdateDoctorAsync(Guid doctorId, DoctorEditItemDto d)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
            if (doctor == null)
                return null;

            doctor.Name = d.Name;
            doctor.LastName = d.LastName;
            doctor.Age = d.Age;

            await _context.SaveChangesAsync();
            return doctor;
        }
    }
}
