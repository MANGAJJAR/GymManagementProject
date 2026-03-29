using GymManagement.API.DTOs;
using GymManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly IGymService _service;

        public TrainersController(IGymService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trainers = await _service.GetAllTrainersAsync();
            return Ok(trainers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainerDto dto)
        {
            var created = await _service.AddTrainerAsync(dto);
            return Ok(created);
        }
    }
}
