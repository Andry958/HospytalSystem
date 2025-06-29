using HospitalSystem.Models.Core;
using HospitalSystem.Models.DB;
using HospitalSystem.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models.Servies
{
    public class PatientEditorC_service
    {
        private readonly HospitalDbContext _context;

        public PatientEditorC_service(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Guid?> ChangePatientAsync(Guid patientId, PatientRequestDto pq)
        {
            var patientToUpdate = await _context.Patients.FirstOrDefaultAsync(i => i.Id == patientId);
            if (patientToUpdate == null)
                return null;

            patientToUpdate.Name = pq.Name;
            patientToUpdate.LastName = pq.LastName;
            patientToUpdate.Age = pq.Age;

            await _context.SaveChangesAsync();
            return patientId;
        }

        public async Task<Guid?> DeletePatientAsync(Guid patientId)
        {
            var patientToDelete = await _context.Patients.FirstOrDefaultAsync(i => i.Id == patientId);
            if (patientToDelete == null)
                return null;

            _context.Patients.Remove(patientToDelete);
            await _context.SaveChangesAsync();
            return patientId;
        }

        public async Task AddPatientAsync(PatientRequestDto pq)
        {
            var patient = new Patient
            {
                Name = pq.Name,
                LastName = pq.LastName,
                Age = pq.Age
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<string> AddDiagnosisAsync(DiagnosisCreateDto d)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == d.PatientId);
            var doctor = await _context.Doctors.FirstOrDefaultAsync(p => p.Id == d.DoctorId);

            if (patient == null || doctor == null)
                return null;

            var disease = new Disease
            {
                Name = d.NameDisease,
                Description = d.DescriptionDisease
            };
            await _context.Disease.AddAsync(disease);

            var visit = new Visit
            {
                VisitDate = d.VisitDate,
                Patient = patient,
                Doctor = doctor
            };
            await _context.Visits.AddAsync(visit);

            var diagnosis = new Diagnosis
            {
                Description = d.Description,
                Disease = disease,
                Visit = visit
            };
            await _context.Diagnosiss.AddAsync(diagnosis);

            await _context.SaveChangesAsync();

            return "Успішно додано діагноз!";
        }

        public async Task<List<Patient>> GetSortedPatientsAsync(string sortedBy)
        {
            var patients = await _context.Patients.ToListAsync();

            return sortedBy switch
            {
                "Name" => patients.OrderBy(p => p.Name).ToList(),
                "LastName" => patients.OrderBy(p => p.LastName).ToList(),
                "Age" => patients.OrderBy(p => p.Age).ToList(),
                "Simply" => patients,

                _ => null
            };
        }
    }
}

