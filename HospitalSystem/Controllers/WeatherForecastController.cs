using HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/register")]

    public class WeatherForecastController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public WeatherForecastController(HospitalDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {

            _context.Doctors.Add(new Doctor
            {
                Name = request.Name,
                LastName = request.LastName,
                Age = request.Age,
                Login = request.Login,
                Password = request.Password
            });
            _context.SaveChanges();
            return Ok(new { name = request.Name });
        }
    }
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

}
