using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GuessIt.Services;
using System.Linq;
using System.Security.Claims;
using GuessIt.Models;
using GuessIt.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace GuessIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQuizController : ControllerBase
    {
        private readonly UserQuizService _userQuizService;
        private readonly QuizService _quizService;
        

        public UserQuizController(UserQuizService userQuizService, QuizService quizService)
        {
            _userQuizService = userQuizService;
            _quizService = quizService;
        }

        [HttpGet("get-all-user-quizzes")]
        public async Task<IActionResult> GetAllUserQuizzes()
        {
            var result = await _userQuizService.GetAllUserQuizzesAsync();
            if (result == null || !result.Any())
            {
                return NotFound("No UserQuizzes found.");
            }
            return Ok(result);
        }

        [HttpGet("get-user-quiz-by-id/{id}")]
        public async Task<IActionResult> GetUserQuizById(int id)
        {
            var result = await _userQuizService.GetUserQuizByIdAsync(id);
            if (result == null)
            {
                return NotFound($"UserQuiz with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost("invite-user-to-quiz")]
        [Authorize]
        public async Task<IActionResult> InviteUserToQuiz([FromBody] InviteToQuizDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var quiz = await _quizService.GetQuizById(dto.QuizId);
            if (quiz == null)
            {
                return NotFound("Quiz not found.");
            }

            if (userId != quiz.CreatorId)
            {
                return Unauthorized("Only the creator can invite users to this quiz.");
            }

            var result = await _userQuizService.InviteUserToQuizAsync(dto);
            return Ok(result);
        }

        [HttpDelete("delete-user-from-quiz")]
        [Authorize]
        public async Task<IActionResult> DeleteUserFromQuiz([FromBody] DeleteUserFromQuizDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var quiz = await _quizService.GetQuizById(dto.QuizId);
            if (quiz == null)
            {
                return NotFound("Quiz not found.");
            }
            if (userId != quiz.CreatorId)
            {
                return Unauthorized("Only the creator can delete users from this quiz.");
            }
            
            var result = await _userQuizService.DeleteUserFromQuizAsync(dto);
            return Ok(result);
        }

        [HttpGet("my-quizzes")]
        [Authorize]
        public async Task<IActionResult> MyQuizzes()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }
            var result = await _userQuizService.MyQuizzesAsync(userId);
            return Ok(result);
        }
    }
}
