using IW5.API.Controllers.Base;
using IW5.BL.API.Contracts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IW5.API.Controllers
{
    
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class UsersController : BaseCrudController<User, UsersController>
    {
        private readonly IUserBLogic _userLogic;
        public UsersController(IServiceManager serviceManager) : base(serviceManager)
        {
            _userLogic = serviceManager.UserService;
        }

        public override ActionResult<IQueryable<User>> GetAll()
        {
            return Ok(_userLogic.GetAll());
        }

        public override async Task<ActionResult> Delete(Guid id)
        {
            var model = await _userLogic.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            try
            {
                _userLogic.Delete(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        public override async Task<ActionResult<IQueryable<User>>> GetById(Guid id)
        {
            return Ok(await _userLogic.GetByIdAsync(id));
        }
        
        public override async Task<ActionResult> Create(Guid id)
        {
            return Ok(await _userLogic.GetByIdAsync(id));
        }

        public override async Task<ActionResult> UpdateAsync(Guid id)
        {
            
            var model = await _userLogic.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            try
            {
                await _userLogic.CreateOrUpdateAsync(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

    }
}
