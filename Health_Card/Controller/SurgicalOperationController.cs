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
    public class SurgicalOperationController : ControllerBase
    {
        private readonly IServiceBase<SurgicalOperation, SurgicalOperationFilter> _surgicalOperationService;

        public SurgicalOperationController(IServiceBase<SurgicalOperation, SurgicalOperationFilter> surgicalOperationService)
        {
            _surgicalOperationService = surgicalOperationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurgicalOperation>>> GetAll([FromQuery] SurgicalOperationFilter filter)
        {

                var surgicalOperations = await _surgicalOperationService.GetAllAsync(filter);
                return Ok(surgicalOperations);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurgicalOperation>> GetById(int id)
        {
            var surgicalOperation = await _surgicalOperationService.GetByIdAsync(id);
            if (surgicalOperation == null) return NotFound();
            return Ok(surgicalOperation);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SurgicalOperation surgicalOperation)
        {
            await _surgicalOperationService.CreateAsync(surgicalOperation);
            return CreatedAtAction(nameof(GetById), new { id = surgicalOperation.OperationID }, surgicalOperation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SurgicalOperation surgicalOperation)
        { 
            if (id != surgicalOperation.OperationID)
                return BadRequest("ID mismatch");
            
            var existing = await _surgicalOperationService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _surgicalOperationService.UpdateAsync(surgicalOperation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _surgicalOperationService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _surgicalOperationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
