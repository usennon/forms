using IW5.API.Controllers.Base;
using IW5.BL.API.Contracts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IW5.API.Controllers
{

    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class OptionsController : BaseCrudController<Option, OptionsController>
    {
        private readonly IOptionBLogic _optionsLogic;
        public OptionsController(IServiceManager serviceManager) : base(serviceManager)
        {
            _optionsLogic = serviceManager.OptionService;
        }

        public override ActionResult<IQueryable<Option>> GetAll()
        {
            return Ok(_optionsLogic.GetAll());
        }

        public override async Task<ActionResult> Delete(Guid id)
        {
            var model = await _optionsLogic.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            try
            {
                _optionsLogic.Delete(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        public override async Task<ActionResult<IQueryable<Option>>> GetById(Guid id)
        {
            return Ok(await _optionsLogic.GetByIdAsync(id));
        }
        
        public override async Task<ActionResult> Create(Guid id)
        {
            return Ok(await _optionsLogic.GetByIdAsync(id));
        }

        public override async Task<ActionResult> UpdateAsync(Guid id)
        {
            
            var model = await _optionsLogic.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            try
            {
                await _optionsLogic.CreateOrUpdateAsync(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

    }
}
