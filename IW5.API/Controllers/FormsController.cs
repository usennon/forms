using IW5.BL.API;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ManipulationModels.FormsModels;
using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IW5.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FormsController : ControllerBase
    {
        private readonly IFormBLogic _formLogic;
        public FormsController(IServiceManager serviceManager)
        {
            _formLogic = serviceManager.FormService;
        }

        [HttpGet("all", Name = "GetAllForms")]
        public ActionResult<IQueryable<Form>> GetAll()
        {
            return Ok(_formLogic.GetAll());
        }
        
        [HttpGet("filtered", Name = "GetSortedOrSearchForms")]
        public ActionResult<IQueryable<Form>> GetFilteredOrSorted(FormSortType type, string searchString = "")
        {
            return Ok(_formLogic.GetFilteredForms(searchString, type));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _formLogic.DeleteAsync(id, false);
            return NoContent();
        }

        [HttpGet("{id:guid}", Name = "FormById")]
        public async Task<ActionResult<IQueryable<Form>>> GetById(Guid id)
        {
            return Ok(await _formLogic.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateForm([FromBody] FormForManipulationDTO form)
        {
            var result = await _formLogic.Create(form);
            return CreatedAtRoute("FormById", new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateFormAsync(Guid id, [FromBody] FormForManipulationDTO form)
        {
            await _formLogic.UpdateAsync(id, form, true);
            return Ok();
        }
    }
}
