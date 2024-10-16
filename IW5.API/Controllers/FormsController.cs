using IW5.API.Controllers.Base;
using IW5.BL.API.Contracts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IW5.API.Controllers
{

    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class FormsController : BaseCrudController<Form, FormsController>
    {
        private readonly IFormBLogic _formLogic;
        public FormsController(IServiceManager serviceManager) : base(serviceManager)
        {
            _formLogic = serviceManager.FormService;
        }

        public override ActionResult<IQueryable<Form>> GetAll()
        {
            return Ok(_formLogic.GetAll());
        }

        public override async Task<ActionResult> Delete(Guid id)
        {
            var model = await _formLogic.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            try
            {
                _formLogic.Delete(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        public override async Task<ActionResult<IQueryable<Form>>> GetById(Guid id)
        {
            return Ok(await _formLogic.GetByIdAsync(id));
        }
        
        public override async Task<ActionResult> Create(Guid id)
        {
            return Ok(await _formLogic.GetByIdAsync(id));
        }

        public override async Task<ActionResult> UpdateAsync(Guid id)
        {
            
            var model = await _formLogic.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            try
            {
                await _formLogic.CreateOrUpdateAsync(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
