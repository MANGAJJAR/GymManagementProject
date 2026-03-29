using GymManagement.API.DTOs;
using GymManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IGymService _service;

        public MembersController(IGymService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _service.GetAllMembersAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _service.GetMemberByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberDto dto)
        {
            var created = await _service.AddMemberAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MemberId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMemberDto dto)
        {
            if (id != dto.MemberId) return BadRequest("ID mismatch");
            await _service.UpdateMemberAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteMemberAsync(id);
            return NoContent();
        }
    }
}
