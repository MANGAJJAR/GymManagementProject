using GymManagement.API.DTOs;
using GymManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IGymService _service;

        public PlansController(IGymService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _service.GetAllPlansAsync();
            return Ok(plans);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlanDto dto)
        {
            var created = await _service.AddPlanAsync(dto);
            return Ok(created);
        }
    }
}
