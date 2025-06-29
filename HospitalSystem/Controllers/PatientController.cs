using HospitalSystem.Models;
using HospitalSystem.Models.Core;
using HospitalSystem.Models.DB;
using HospitalSystem.Models.Dto;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientController : Controller
    {
        private readonly HospitalDbContext _context;
        public PatientController(HospitalDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetPatient()
        {
            var patients = await _context.Patients.ToListAsync();
            return Ok(patients);
        }

        [HttpPost]
        public async Task<IActionResult> PostPatient([FromBody] PatientRequestDto pr)
        {
            var newPatient = new Patient
            {
                Name = pr.Name,
                LastName = pr.LastName,
                Age = pr.Age
            };

            await _context.Patients.AddAsync(newPatient);
            await _context.SaveChangesAsync();

            return Ok(newPatient);
        }

    }
}
