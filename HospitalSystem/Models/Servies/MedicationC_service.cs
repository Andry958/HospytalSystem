using HospitalSystem.Models.Core;
using HospitalSystem.Models.DB;
using HospitalSystem.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models.Services
{
    public class MedicationC_service
    {
        private readonly HospitalDbContext _context;

        public MedicationC_service(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medication>> GetAllMedicationsAsync()
        {
            return await _context.Medications.ToListAsync();
        }

        public async Task<Medication> AddMedicationAsync(MedicationRequestDto mq)
        {
            var medication = new Medication
            {
                Name = mq.Name,
                Description = mq.Description,
                Quantity = mq.Quantity
            };

            await _context.Medications.AddAsync(medication);
            await _context.SaveChangesAsync();

            return medication;
        }
        public async Task<Guid> Remove(Guid medicationId)
        {
            var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == medicationId);
            if (medication == null)
                throw new KeyNotFoundException("Medication not found");

            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();

            return medicationId;
        }   
        public async Task<Medication?> UpdateMedicationAsync(Guid medicationId, MedicationRequestDto mq)
        {
            var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == medicationId);
            if (medication == null)
                return null;

            medication.Name = mq.Name;
            medication.Description = mq.Description;
            medication.Quantity = mq.Quantity;

            await _context.SaveChangesAsync();
            return medication;
        }
        public async Task<List<Medication>> Sorted(string SortedBy)
        {
            var m = await _context.Medications.ToListAsync();

            return SortedBy switch
            {
                "Name" => m.OrderBy(p => p.Name).ToList(),
                "Description" => m.OrderBy(p => p.Description).ToList(),
                "Quantity" => m.OrderBy(p => p.Quantity).ToList(),
                "Simply" => m,
                _ => null
            };
        }
    }
}
