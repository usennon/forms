using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IW5.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionBLogic _questionsLogic;
        public QuestionsController(IServiceManager serviceManager)
        {
            _questionsLogic = serviceManager.QuestionService;
        }

        [HttpGet("all", Name = "GetAllQuestions")]
        public ActionResult<IQueryable<Question>> GetAll()
        {
            return Ok(_questionsLogic.GetAll());
        }

        [HttpPatch("filtered", Name = "GetSortedOrSearchQuestions")]
        public ActionResult<IQueryable<Form>> GetFilteredOrSorted(string searchString = "", QuestionSortType type = QuestionSortType.None)
        {
            return Ok(_questionsLogic.GetFilteredQuestions(searchString, type));
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _questionsLogic.DeleteAsync(id, false);
            return NoContent();
        }

        [HttpGet("{id:guid}", Name = "QuestionById")]
        public async Task<ActionResult<IQueryable<Question>>> GetById(Guid id)
        {
            return Ok(await _questionsLogic.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] QuestionDetailModel question)
        {
            var result = await _questionsLogic.Create(question);
            return CreatedAtRoute("QuestionById", new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateQuestionAsync(Guid id, [FromBody] QuestionDetailModel question)
        {
            await _questionsLogic.UpdateAsync(id, question, true);
            return Ok();
        }


    }
}
