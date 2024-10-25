using IW5.BL.API;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ManipulationModels.QuestionModels;
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
            var question = await _questionsLogic.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] QuestionForManipulationDTO question)
        {
            var result = await _questionsLogic.Create(question);
            return CreatedAtRoute("QuestionById", new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateQuestionAsync(Guid id, [FromBody] QuestionForManipulationDTO question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingQuestion = await _questionsLogic.GetByIdAsync(id);
            if (existingQuestion == null)
            {
                return NotFound();
            }
            await _questionsLogic.UpdateAsync(id, question, true);
            return Ok();
        }


    }
}
