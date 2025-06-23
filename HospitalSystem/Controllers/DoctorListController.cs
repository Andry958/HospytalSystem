using HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorListController : Controller
    {
        private readonly HospitalDbContext _context;

        public DoctorListController(HospitalDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _context.Doctors.ToList();
            return Ok(doctors);
        }
    }
}
