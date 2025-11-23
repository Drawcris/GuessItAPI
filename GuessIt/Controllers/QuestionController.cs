using System.Security.Claims;
using GuessIt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GuessIt.DTOs;

namespace GuessIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly QuizService _quizService;
        
        public QuestionController(QuestionService questionService, QuizService quizService)
        {
            _questionService = questionService;
            _quizService = quizService;
        }

        [HttpGet("get-all-questions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _questionService.GetAllQuestionsAsync();
            if (!questions.Any())
            {
                return NotFound("No questions found");
            }
            return Ok(questions);
        }

        [HttpGet("get-question/{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound("Question not found");
            }
            return Ok(question);
        }

        [HttpDelete("delete-question/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (question == null)
            {
                return NotFound("Question not found");
            }
            var quiz = await _quizService.GetQuizById(question.QuizId);
            if (quiz == null)
            {
                return NotFound("Quiz not found");
            }
            if (quiz.CreatorId != userId)
            {
                return Unauthorized("You are not authorized to delete this question");
            }
            
            var deletedQuestion = await _questionService.DeleteQuestionAsync(id);
            return Ok(deletedQuestion);
        }

        [HttpPost("add-question")]
        [Authorize]
        public async Task<IActionResult> AddQuestion([FromForm] AddQuestionDTO dto)
        {
            var result = await _questionService.AddQuestionAsync(dto);
            return Ok(result);
        }
    }
}
