using HospitalSystem.Models.DB;
using HospitalSystem.Models.Dto;
using HospitalSystem.Models.Servies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorListController : Controller
    {
        private readonly DoctorsC_service _service;

        public DoctorListController(DoctorsC_service service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _service.GetAllDoctorsAsync();
            return Ok(doctors);
        }
        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> RemoveDoctor(Guid doctorId)
        {
            try
            {
                var removedId = await _service.Deleted(doctorId);
                return Ok(new { Id = removedId });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctor(Guid doctorId, [FromBody] DoctorEditItemDto d)
        {
            var updatedDoctor = await _service.UpdateDoctorAsync(doctorId, d);
            if (updatedDoctor == null)
                return NotFound("Doctor not found");

            return Ok(new
            {
                updatedDoctor.Id,
                updatedDoctor.Name,
                updatedDoctor.LastName,
                updatedDoctor.Age
            });
        }
    }
}
