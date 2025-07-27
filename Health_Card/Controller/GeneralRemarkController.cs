using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Model;
using Microsoft.AspNetCore.Mvc;
using Health_Card.Interface;

namespace Health_Card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralRemarkController : ControllerBase
    {
        private readonly  IServiceBase<GeneralRemark, GeneralRemarkFilter> _generalRemarkService;

        public GeneralRemarkController( IServiceBase<GeneralRemark, GeneralRemarkFilter> generalRemarkService)
        {
            _generalRemarkService = generalRemarkService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneralRemark>>> GetAll([FromQuery] GeneralRemarkFilter filter)
        {

                var generalRemarks = await _generalRemarkService.GetAllAsync(filter);
                return Ok(generalRemarks);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralRemark>> GetById(int id)
        {
            var generalRemark = await _generalRemarkService.GetByIdAsync(id);
            if (generalRemark == null) return NotFound();
            return Ok(generalRemark);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] GeneralRemark generalRemark)
        {
            await _generalRemarkService.CreateAsync(generalRemark);
            return CreatedAtAction(nameof(GetById), new { id = generalRemark.RemarkID }, generalRemark);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GeneralRemark generalRemark)
        { 
            if (id != generalRemark.RemarkID)
                return BadRequest("ID mismatch");
            
            var existing = await _generalRemarkService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _generalRemarkService.UpdateAsync(generalRemark);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _generalRemarkService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _generalRemarkService.DeleteAsync(id);
            return NoContent();
        }
    }
}
