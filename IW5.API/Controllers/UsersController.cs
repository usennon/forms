using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IW5.API.Controllers
{
    
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserBLogic _userLogic;
        public UsersController(IServiceManager serviceManager)
        {
            _userLogic = serviceManager.UserService;
        }

        [HttpGet(Name = "GetUsers")]
        public ActionResult<IQueryable<User>> GetAll()
        {
            return Ok(_userLogic.GetAll());
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userLogic.DeleteAsync(id, false);
            return NoContent();
        }

        [HttpGet("{id:guid}", Name = "UserById")]
        public async Task<ActionResult<IQueryable<User>>> GetById(Guid id)
        {
            return Ok(await _userLogic.GetByIdAsync(id));
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] UserDetailModel user)
        {
            var result = await _userLogic.Create(user);
            return CreatedAtRoute("UserById", new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateUserAsync(Guid id, [FromBody] UserDetailModel user)
        {

            await _userLogic.UpdateAsync(id, user, true);
            return Ok();
        }

    }
}
