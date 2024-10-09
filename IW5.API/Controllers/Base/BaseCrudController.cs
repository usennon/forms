using IW5.DAL.Contracts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IW5.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseCrudController<T, TController> : ControllerBase
        where T : BaseEntity, new()
        where TController : BaseCrudController<T, TController>
    {
        protected readonly IRepo<T> MainRepo;


        protected BaseCrudController(IRepo<T> repo)
        {
            MainRepo = repo;
        }

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
        public ActionResult<IQueryable<T>> GetAll()
        {
            return Ok(MainRepo.GetAll(false));
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpDelete("{Id}")]
        public ActionResult Delete(T entity)
        {
            MainRepo.Delete(entity);
            return Ok();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [SwaggerResponse(404, "User was not found")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IQueryable<T>>> GetById(Guid id)
        {
            var entity = await MainRepo.GetByIdAsync(id, false);
            return entity == null ? NotFound() : Ok();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPost]
        public ActionResult Create(T entity)
        {
            MainRepo.Create(entity);
            return Ok();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPut]
        public ActionResult Update(T entity)
        {
            MainRepo.Update(entity);
            return Ok();
        }

    }
}
