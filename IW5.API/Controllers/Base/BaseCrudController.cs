using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IW5.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseCrudController<TEntity, TListModel, TDetailModel> : ControllerBase
        where TEntity : BaseEntity, new()
        where TListModel : ListModelBase
        where TDetailModel : DetailModelBase
    {
        private readonly IServiceManager _service;
        protected BaseCrudController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets all records
        /// </summary>
        /// <returns>All records</returns>
        /// <response code="200">Returns all items</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpGet]
        public virtual ActionResult<IQueryable<T>> GetAll()
        {
            return Ok();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpDelete("{Id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            return await Task.FromResult(Ok());
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [SwaggerResponse(404, "User was not found")]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<IQueryable<T>>> GetById(Guid id)
        {
            return await Task.FromResult(Ok());
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPost]
        public virtual async Task<ActionResult> Create(Guid id)
        {
            return await Task.FromResult(Ok());
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPut]
        public virtual async Task<ActionResult> UpdateAsync(Guid id)
        {
            return await Task.FromResult(Ok());
        }

    }
    }
