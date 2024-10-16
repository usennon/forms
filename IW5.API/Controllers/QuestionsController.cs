using IW5.API.Controllers.Base;
using IW5.BL.API.Contracts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IW5.API.Controllers
{

    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class QuestionsController : BaseCrudController<Question, QuestionsController>
    {
        private readonly IQuestionBLogic _questionsLogic;
        public QuestionsController(IServiceManager serviceManager) : base(serviceManager)
        {
            _questionsLogic = serviceManager.QuestionService;
        }

        public override ActionResult<IQueryable<Question>> GetAll()
        {
            return Ok(_questionsLogic.GetAll());
        }

        public override async Task<ActionResult> Delete(Guid id)
        {
            var model = await _questionsLogic.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            try
            {
                _questionsLogic.Delete(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        public override async Task<ActionResult<IQueryable<Question>>> GetById(Guid id)
        {
            return Ok(await _questionsLogic.GetByIdAsync(id));
        }
        
        public override async Task<ActionResult> Create(Guid id)
        {
            return Ok(await _questionsLogic.GetByIdAsync(id));
        }

        public override async Task<ActionResult> UpdateAsync(Guid id)
        {
            
            var model = await _questionsLogic.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            try
            {
                await _questionsLogic.CreateOrUpdateAsync(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }


    }
}
