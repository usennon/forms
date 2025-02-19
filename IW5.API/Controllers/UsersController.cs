﻿using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels.UserModels;
using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IW5.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserBLogic _userLogic;
        public UsersController(IUserBLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet("all", Name = "GetUsers")]

        public ActionResult<IQueryable<User>> GetAll()
        {
            return Ok(_userLogic.GetAll());
        }
        
        [HttpGet("filtered", Name = "GetSortedOrSearchUsers")]

        public ActionResult<IQueryable<Form>> GetFilteredOrSorted(UserSortType type, string searchString = "")
        {
            return Ok(_userLogic.GetFilteredUsers(searchString, type));
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
            var user = await _userLogic.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost(Name = "CreateUser")]

        public async Task<ActionResult> CreateUser([FromBody] UserForManipulationModel user)
        {
            try
            {
                var result = await _userLogic.Create(user);
                return CreatedAtRoute("UserById", new { id = result.Id }, result);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id:guid}")]

        public async Task<ActionResult> UpdateUserAsync(Guid id, [FromBody] UserForManipulationModel user)
        {
            try
            {
                await _userLogic.UpdateAsync(id, user, true);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
