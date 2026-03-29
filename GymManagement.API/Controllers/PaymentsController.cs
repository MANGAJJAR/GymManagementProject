using GymManagement.API.DTOs;
using GymManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IGymService _service;

        public PaymentsController(IGymService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _service.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentDto dto)
        {
            var created = await _service.RecordPaymentAsync(dto);
            return Ok(created);
        }
    }
}
