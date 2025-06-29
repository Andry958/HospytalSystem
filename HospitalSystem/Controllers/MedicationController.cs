using HospitalSystem.Models.Dto;
using HospitalSystem.Models.Services;
using HospitalSystem.Models.Servies;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/medication")]
    public class MedicationController : Controller
    {
        private readonly MedicationC_service _service;

        public MedicationController(MedicationC_service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedications()
        {
            var medications = await _service.GetAllMedicationsAsync();
            return Ok(medications);
        }

        [HttpPost]
        public async Task<IActionResult> PostMedications([FromBody] MedicationRequestDto mq)
        {
            var medication = await _service.AddMedicationAsync(mq);
            return Ok(new
            {
                medication.Id,
                medication.Name,
                medication.Description,
                medication.Quantity
            });
        }
        [HttpDelete("{medicationId}")]
        public async Task<IActionResult> RemoveMedication(Guid medicationId)
        {
            try
            {
                Console.WriteLine(medicationId);
                var removedId = await _service.Remove(medicationId);
                return Ok(new { Id = removedId });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{medicationId}")]
        public async Task<IActionResult> UpdateMedication(Guid medicationId, [FromBody] MedicationRequestDto mq)
        {
            var updatedMedication = await _service.UpdateMedicationAsync(medicationId, mq);
            if (updatedMedication == null)
                return NotFound("Medication not found");

            return Ok(new
            {
                updatedMedication.Id,
                updatedMedication.Name,
                updatedMedication.Description,
                updatedMedication.Quantity
            });
        }
        [HttpGet("sorted")]
        public async Task<IActionResult> Sorted([FromQuery] string SortedBy)
        {
            var medications = await _service.Sorted(SortedBy);
            if (medications == null)
                return BadRequest("Unknown sorting parameter.");
            return Ok(medications);
        }
    }
}
