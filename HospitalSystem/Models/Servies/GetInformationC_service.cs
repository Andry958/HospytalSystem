using HospitalSystem.Models.DB;
using HospitalSystem.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models.Services
{
    public class GetInformationC_service
    {
        private readonly HospitalDbContext _context;

        public GetInformationC_service(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<InformationAboutUserDto?> GetInformationAsync(int index)
        {
            var patientId = await GetPatientIdByIndexAsync(index);
            if (patientId == null)
                return null;

            var patient = await _context.Diagnosiss
                .Where(d => d.Visit.Patient.Id == patientId)
                .Select(d => d.Visit.Patient)
                .FirstOrDefaultAsync();

            var descriptions = await _context.Diagnosiss
                .Where(d => d.Visit.Patient.Id == patientId)
                .Select(d => d.Description)
                .ToListAsync();

            var doctors = await _context.Diagnosiss
                .Where(d => d.Visit.Patient.Id == patientId)
                .Select(d => d.Visit.Doctor)
                .ToListAsync();

            var nameDiseases = await _context.Diagnosiss
                .Where(d => d.Visit.Patient.Id == patientId)
                .Select(d => d.Disease.Name)
                .ToListAsync();

            var descriptionDiseases = await _context.Diagnosiss
                .Where(d => d.Visit.Patient.Id == patientId)
                .Select(d => d.Disease.Description)
                .ToListAsync();

            var visitDates = await _context.Diagnosiss
                .Where(d => d.Visit.Patient.Id == patientId)
                .Select(d => d.Visit.VisitDate)
                .ToListAsync();

            return new InformationAboutUserDto
            {
                Description = descriptions,
                NameDisease = nameDiseases,
                DescriptionDisease = descriptionDiseases,
                VisitDate = visitDates,
                Doctor = doctors,
                Patient = patient
            };
        }

        private async Task<Guid?> GetPatientIdByIndexAsync(int index)
        {
            var patients = await _context.Patients.ToListAsync();
            if (index < 0 || index >= patients.Count)
                return null;

            return patients[index].Id;
        }
    }
}