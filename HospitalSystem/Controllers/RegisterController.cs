using HospitalSystem.Models.Dto;
using HospitalSystem.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterC_service _registerService;

        public RegisterController(RegisterC_service registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var (success, newDoctor) = await _registerService.RegisterDoctorAsync(request);

            if (!success)
            {
                return BadRequest();
            }

            return Ok(newDoctor);
        }
    }
}
