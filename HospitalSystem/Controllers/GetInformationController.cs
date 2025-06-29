using HospitalSystem.Models.Dto;
using HospitalSystem.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/informationAboutUser")]
    public class GetInformationController : Controller
    {
        private readonly GetInformationC_service _service;

        public GetInformationController(GetInformationC_service service)
        {
            _service = service;
        }

        [HttpGet("{pation}")]
        public async Task<IActionResult> GetInformation(int pation)
        {
            var information = await _service.GetInformationAsync(pation);
            if (information == null)
                return NotFound("Пацієнта не знайдено або індекс некоректний.");

            return Ok(information);
        }
    }
}
