using IW5.API.Common;
using IW5.BL.API;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ManipulationModels.FormsModels;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Contracts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly IRepositoryManager _answerRepo;

    public AnswersController(IRepositoryManager answerRepo)
    {
        _answerRepo = answerRepo;
    }

    [HttpGet("all", Name = "GetAllAnswers")]
    public async Task<ActionResult<List<Answer>>> GetAll()
    {
        var answers = _answerRepo.Answer.GetAll(false);
        return Ok(answers);
    }
    [HttpGet("{id:guid}", Name = "AnswerById")]
    public async Task<ActionResult<Answer>> GetById(Guid id)
    {
        var answer = await _answerRepo.Answer.GetByIdAsync(id,false);
        if (answer == null)
        {
            return NotFound();
        }

        return Ok(answer);
    }

}
