using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface;
using Health_Card.Model;
using Microsoft.AspNetCore.Mvc;

namespace Health_Card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServantController : ControllerBase
    {
        private readonly IServantService _servantService;

        public ServantController(IServantService servantService)
        {
            _servantService = servantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servant>>> GetAll()
        {
            var servants = await _servantService.GetAllAsync();
            return Ok(servants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servant>> GetById(int id)
        {
            var servant = await _servantService.GetByIdAsync(id);
            if (servant == null) return NotFound();
            return Ok(servant);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Servant servant)
        {
            await _servantService.CreateAsync(servant);
            return CreatedAtAction(nameof(GetById), new { id = servant.ServantID }, servant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Servant servant)
        {
            if (id != servant.ServantID)
                return BadRequest("ID mismatch");
            
            var existing = await _servantService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _servantService.UpdateAsync(servant);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _servantService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _servantService.DeleteAsync(id);
            return NoContent();
        }
    }
}
