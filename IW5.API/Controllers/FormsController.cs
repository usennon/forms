using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ManipulationModels.FormsModels;
using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<Form>>> GetAll()
    {
        var forms =  _formLogic.GetAll();
        return Ok(forms);
    }

    [HttpGet("filter/{type}/{searchString?}")]
    public ActionResult<List<Form>> GetFilteredOrSorted(FormSortType type, string searchString = "")
    {
        var forms = _formLogic.GetFilteredForms(searchString, type);
        return Ok(forms.ToList());
    }

    [HttpGet("{id:guid}", Name = "FormById")]
    public async Task<ActionResult<Form>> GetById(Guid id)
    {
        var form = await _formLogic.GetByIdAsync(id);
        if (form == null)
        {
            return NotFound();
        }

        return Ok(form);
    }

    [HttpPost]
    public async Task<ActionResult> CreateForm([FromBody] FormForManipulationModel form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _formLogic.Create(form);
        return CreatedAtRoute("FormById", new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateFormAsync(Guid id, [FromBody] FormForManipulationModel form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingForm = await _formLogic.GetByIdAsync(id);
        if (existingForm == null)
        {
            return NotFound();
        }

        await _formLogic.UpdateAsync(existingForm.Id, form, true);

        return Ok(); 
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var form = await _formLogic.GetByIdAsync(id);
        if (form == null)
        {
            return NotFound();
        }

        await _formLogic.DeleteAsync(id, false);
        return Ok();
    }
}
