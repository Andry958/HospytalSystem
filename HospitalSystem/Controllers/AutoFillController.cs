using HospitalSystem.Models.Core;
using HospitalSystem.Models.DB;
using HospitalSystem.Models.Servies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/autofill")]
    public class AutoFillController : Controller
    {
        private readonly HospitalDbContext _context;
        public AutoFillController(HospitalDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = new AutoFillC_service(_context);
            var data = await service.GetData();
            return Ok(data);

        }


        [HttpPost]
        public async Task<IActionResult>  GetAll() {

            var service = new AutoFillC_service(_context);
            var result = await service.FillData();
            return Ok(result);
        }
        static DateTime RandomDate()
        {
            var rnd = new Random();
            int daysAgo = rnd.Next(1, 365);
            return DateTime.Now.Date.AddDays(-daysAgo);
        }

    }
}
