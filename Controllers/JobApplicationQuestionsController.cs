using JobApplicationAPI.Models;
using JobApplicationAPI.Models.DTOs;
using JobApplicationAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JobApplicationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobApplicationQuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;
        public JobApplicationQuestionsController(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public ActionResult<List<QuestionContentDTO>> Get()
        {
            try
            {
                var questions = _questionService.GetQuestions();
                var questionsDTO = new List<QuestionContentDTO>();
                mapQuestions(questions, questionsDTO);

                return questionsDTO;
            }
            catch
            {
                // log here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred when trying to GET Questions. It has been logged and is being looked into");
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] QuestionContent question)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_questionService.GetQuestion(question.Id) != null)
                {
                    return Conflict($"Question with this Id: {question.Id} already exists");
                }

                _questionService.CreateQuestion(question);
                var questionDTO = new QuestionContentDTO();
                mapQuestion(question, questionDTO);
                return Created("", questionDTO);
            }
            catch
            {
                // log here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred when trying to POST a Question. It has been logged and is being looked into");
            }

        }

        // this would be handled by a mapper like AutoMapper or custom Mapper implementation if I had more time
        private void mapQuestions(List<QuestionContent> questions, List<QuestionContentDTO> questionsDTO)
        {
            foreach (var question in questions)
            {
                questionsDTO.Add(new QuestionContentDTO()
                {
                    Id = question.Id,
                    Question = question.Question,
                    AcceptedAnswers = question.AcceptedAnswers
                });
            }
        }

        private void mapQuestion(QuestionContent question, QuestionContentDTO questionDTO)
        {
            questionDTO.Id = question.Id;
            questionDTO.Question = question.Question;
            questionDTO.AcceptedAnswers = question.AcceptedAnswers;
        }
    }
}
