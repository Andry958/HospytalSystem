using HospitalSystem.Models.Dto;
using HospitalSystem.Models.Services;
using HospitalSystem.Models.Servies;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : Controller
    {
        private readonly LoginC_service _service;

        public LoginController(LoginC_service service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Login and password must be provided.");
            }

            var response = await _service.AuthenticateAsync(request);

            if (response == null)
            {
                return Unauthorized("Invalid login or password.");
            }

            return Ok(response);
        }
    }
}
