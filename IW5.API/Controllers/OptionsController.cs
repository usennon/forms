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
            return Ok(options); // Return 200 OK with the list of options
        }

        // GET: api/Options/{id}
        [HttpGet("{id:guid}", Name = "OptionById")]
        public async Task<ActionResult<Option>> GetById(Guid id)
        {
            var option = await _optionsLogic.GetByIdAsync(id);
            if (option == null)
            {
                return NotFound($"Option with ID {id} not found."); // Return 404 Not Found if no option is found
            }

            return Ok(option); // Return 200 OK with the option data
        }

        // POST: api/Options
        [HttpPost]
        public async Task<ActionResult> CreateOption([FromBody] OptionForManipulationDTO option)
        {
            if (option == null)
            {
                return BadRequest("Option data is null."); // Return 400 Bad Request if input is null
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request if the model is invalid
            }

            var createdOption = await _optionsLogic.Create(option);
            return CreatedAtRoute("OptionById", new { id = createdOption.Id }, createdOption); // Return 201 Created with the location of the new option
        }

        // PUT: api/Options/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateOptionAsync(Guid id, [FromBody] OptionForManipulationDTO option)
        {
            if (option == null)
            {
                return BadRequest("Invalid data."); // Return 400 Bad Request if input data is invalid
            }

            var optionExists = await _optionsLogic.GetByIdAsync(id);
            if (optionExists == null)
            {
                return NotFound($"Option with ID {id} not found."); // Return 404 Not Found if no option is found for update
            }

            await _optionsLogic.UpdateAsync(id, option, true);
            return Ok(); // Return 200 OK after successful update
        }

        // DELETE: api/Options/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var option = await _optionsLogic.GetByIdAsync(id);
            if (option == null)
            {
                return NotFound($"Option with ID {id} not found."); // Return 404 Not Found if the option to delete doesn't exist
            }

            await _optionsLogic.DeleteAsync(id, false);
            return NoContent(); // Return 204 No Content after successful deletion
        }
    }
}
