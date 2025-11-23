using System.Security.Claims;
using GuessIt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GuessIt.DTOs;
using GuessIt.Models;

namespace GuessIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;
        
        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("get-all-quizzes")]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var result = await _quizService.GetAllQuizzes();
            
            if (!System.Linq.Enumerable.Any(result))
            {
                return NotFound("No quizzes found.");
            }
            
            return Ok(result);
        }

        [HttpGet("get-quiz-by-id/{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var result = await _quizService.GetQuizById(id);
            
            if (result == null)
            {
                return NotFound("Quiz not found.");
            }
            
            return Ok(result);
        }
        
        [HttpPost("create-quiz")]
        [Authorize]
        public async Task<IActionResult> CreateQuiz([FromForm] CreateQuizDTO quizDto, IFormFile? imageFile)
        {
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (creatorId == null)
            {
                return Unauthorized("User ID not found in token.");
            }
            
            
            var result = await _quizService.CreateQuiz(quizDto, creatorId, imageFile);
            return CreatedAtAction(nameof(GetQuizById), new { id = result.Id }, result);
        }

        [HttpPut("update-quiz/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] UpdateQuizDTO quizDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var quiz = await _quizService.GetQuizById(id);

            if (quiz == null)
            {
                return NotFound("Quiz not found.");
            }
            if (quiz.CreatorId != userId)
            {
                return BadRequest("You are not authorized to update this quiz.");
            }
            
            var result = await _quizService.UpdateQuiz(id, quizDto);
            return Ok(result);
        }

        [HttpDelete("delete-quiz/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var quiz = await _quizService.GetQuizById(id);
            
            if (quiz == null)
            {
                return NotFound("Quiz not found.");
            }
            if (quiz.CreatorId != userId)
            {
                return BadRequest("You are not authorized to delete this quiz.");
            }
            
            var result = await _quizService.DeleteQuizAsync(id);
            return Ok(result);
        }

        [HttpGet("get-questions-in-quiz/{quizId}")]
        public async Task<IActionResult> GetQuestionsInQuiz(int quizId)
        {
            var result = await _quizService.GetQuestionsInQuiz(quizId);
            if (!System.Linq.Enumerable.Any(result))
            {
                return NotFound("No questions found in this quiz.");
            }
            return Ok(result);
        }
    }
}
