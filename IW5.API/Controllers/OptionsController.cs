using IW5.BL.API;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ManipulationModels.OptionModels;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IW5.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionBLogic _optionsLogic;

        public OptionsController(IServiceManager serviceManager)
        {
            _optionsLogic = serviceManager.OptionService;
        }

        // GET: api/Options
        [HttpGet("all")]
        public ActionResult<IQueryable<Option>> GetAll()
        {
            var options = _optionsLogic.GetAll();
            return Ok(options); 
        }

        // GET: api/Options/{id}
        [HttpGet("{id:guid}", Name = "OptionById")]
        public async Task<ActionResult<Option>> GetById(Guid id)
        {
            var option = await _optionsLogic.GetByIdAsync(id);
            if (option == null)
            {
                return NotFound($"Option with ID {id} not found.");
            }

            return Ok(option);
        }

        // POST: api/Options
        [HttpPost]
        public async Task<ActionResult> CreateOption([FromBody] OptionForManipulationModel option)
        {
            if (option == null)
            {
                return BadRequest("Option data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var createdOption = await _optionsLogic.Create(option);
            return CreatedAtRoute("OptionById", new { id = createdOption.Id }, createdOption);
        }

        // PUT: api/Options/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateOptionAsync(Guid id, [FromBody] OptionForManipulationModel option)
        {
            if (option == null)
            {
                return BadRequest("Invalid data.");
            }

            var optionExists = await _optionsLogic.GetByIdAsync(id);
            if (optionExists == null)
            {
                return NotFound($"Option with ID {id} not found.");
            }

            await _optionsLogic.UpdateAsync(id, option, true);
            return Ok();
        }

        // DELETE: api/Options/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var option = await _optionsLogic.GetByIdAsync(id);
            if (option == null)
            {
                return NotFound($"Option with ID {id} not found.");
            }

            await _optionsLogic.DeleteAsync(id, false);
            return NoContent(); // Return 204 No Content after successful deletion
        }
    }
}
