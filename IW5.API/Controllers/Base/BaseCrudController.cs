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
        protected readonly IBLogic<TEntity, TListModel, TDetailModel> _service;
        protected BaseCrudController(IServiceManager serviceManager) => _service = serviceManager.GetService<TEntity, TListModel, TDetailModel>();

        ///// <summary>
        ///// Gets all records
        ///// </summary>
        ///// <returns>All records</returns>
        ///// <response code="200">Returns all items</response>
        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerResponse(200, "The execution was successful")]
        //[SwaggerResponse(400, "The request was invalid")]
        //[HttpGet]
        //public ActionResult<IQueryable<T>> GetAll()
        //{
        //    return Ok(_service.GetAll(false));
        //}

        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerResponse(200, "The execution was successful")]
        //[SwaggerResponse(400, "The request was invalid")]
        //[HttpDelete("{Id}")]
        //public ActionResult Delete(T entity)
        //{
        //    _service.Delete(entity);
        //    return Ok();
        //}

        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerResponse(200, "The execution was successful")]
        //[SwaggerResponse(400, "The request was invalid")]
        //[SwaggerResponse(404, "User was not found")]
        //[HttpGet("{id}")]
        //public async Task<ActionResult<IQueryable<T>>> GetById(Guid id)
        //{
        //    var entity = await _service.GetByIdAsync(id, false);
        //    return entity == null ? NotFound() : Ok();
        //}

        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerResponse(200, "The execution was successful")]
        //[SwaggerResponse(400, "The request was invalid")]
        //[HttpPost]
        //public ActionResult Create(T entity)
        //{
        //    _service.Create(entity);
        //    return Ok();
        //}

        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerResponse(200, "The execution was successful")]
        //[SwaggerResponse(400, "The request was invalid")]
        //[HttpPut]
        //public async Task<ActionResult> UpdateAsync(T entity)
        //{
        //    _service.Update(entity);
        //    return Ok();
        //}

    }
    }
