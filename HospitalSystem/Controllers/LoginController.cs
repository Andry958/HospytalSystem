using HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : Controller
    {
        private readonly HospitalDbContext _context;

        public LoginController(HospitalDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Login and password must be provided.");
            }
            var doctor = _context.Doctors.FirstOrDefault(d => d.Login == request.Login && d.Password == request.Password);
            if (doctor == null)
            {
                return Unauthorized("Invalid login or password.");
            }
            var response = new LoginResponse
            {
                DoctorId = doctor.Id,
                Name = doctor.Name,
                LastName = doctor.LastName,
                Age = doctor.Age
            };
            return Ok(response);
        }

        public class LoginRequest
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }
        public class LoginResponse
        {
            public Guid DoctorId { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }
    }
}
