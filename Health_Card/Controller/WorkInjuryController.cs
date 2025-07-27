using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;
using Microsoft.AspNetCore.Mvc;

namespace Health_Card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkInjuryController : ControllerBase
    {
        private readonly IServiceBase<WorkInjury,WorkInjuryFilter> _workInjuryService;

        public WorkInjuryController(IServiceBase<WorkInjury,WorkInjuryFilter> workInjuryService)
        {
            _workInjuryService = workInjuryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkInjury>>> GetAll([FromQuery] WorkInjuryFilter filter)
        {
                var workInjuries = await _workInjuryService.GetAllAsync(filter);
                return Ok(workInjuries);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkInjury>> GetById(int id)
        {
            var workInjury = await _workInjuryService.GetByIdAsync(id);
            if (workInjury == null) return NotFound();
            return Ok(workInjury);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] WorkInjury workInjury)
        {
            await _workInjuryService.CreateAsync(workInjury);
            return CreatedAtAction(nameof(GetById), new { id = workInjury.InjuryID }, workInjury);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkInjury workInjury)
        { 
            if (id != workInjury.InjuryID)
                return BadRequest("ID mismatch");
            
            var existing = await _workInjuryService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _workInjuryService.UpdateAsync(workInjury);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _workInjuryService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _workInjuryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
