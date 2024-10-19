using IW5.BL.API;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
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

        [HttpGet]
        public ActionResult<IQueryable<Option>> GetAll()
        {
            return Ok(_optionsLogic.GetAll());
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _optionsLogic.DeleteAsync(id, false);
            return NoContent();
        }

        [HttpGet("{id:guid}", Name = "OptionById")]
        public async Task<ActionResult<IQueryable<Option>>> GetById(Guid id)
        {
            return Ok(await _optionsLogic.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateOption([FromBody] OptionDetailModel option)
        {
            var result = await _optionsLogic.Create(option);
            return CreatedAtRoute("OptionById", new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateOptionAsync(Guid id, [FromBody] OptionDetailModel option)
        {
            await _optionsLogic.UpdateAsync(id, option, true);
            return Ok();
        }

    }
}
