using HospitalSystem.Models.Dto;
using HospitalSystem.Models.Services;
using HospitalSystem.Models.Servies;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/patientsEd")]
    public class PatientEditorController : Controller
    {
        private readonly PatientEditorC_service _service;

        public PatientEditorController(PatientEditorC_service service)
        {
            _service = service;
        }

        [HttpPut("{patientId}")]
        public async Task<IActionResult> Change(Guid patientId, [FromBody] PatientRequestDto pq)
        {
            try
            {
                var result = await _service.ChangePatientAsync(patientId, pq);
                if (result == null)
                    return NotFound($"Пацієнт з ID {patientId} не знайдений.");
                return Ok(patientId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка сервера: {ex.Message}");
            }
        }

        [HttpDelete("{patientId}")]
        public async Task<IActionResult> Delete(Guid patientId)
        {
            var result = await _service.DeletePatientAsync(patientId);
            if (result == null)
                return NotFound($"Пацієнт з ID {patientId} не знайдений.");
            return Ok(patientId);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PatientRequestDto pq)
        {
            await _service.AddPatientAsync(pq);
            return Ok("Успішно додано!");
        }

        [HttpPost("diagnosis")]
        public async Task<IActionResult> AddDiagnos([FromBody] DiagnosisCreateDto d)
        {
            var result = await _service.AddDiagnosisAsync(d);
            if (result == null)
                return NotFound("Пацієнт або лікар не знайдені.");
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Sorted([FromQuery] string sortedBy)
        {
            var patients = await _service.GetSortedPatientsAsync(sortedBy);
            if (patients == null)
                return BadRequest("Невідомий параметр сортування.");
            return Ok(patients);
        }
    }
}
