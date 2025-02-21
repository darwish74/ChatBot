using ChatBot.DTOS;
using ChatBot.Models;
using ChatBot.Repositories;
using ChatBot.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly GeminiService _geminiService;
        private readonly MessageRepository _messageRepository;
        private readonly UserManager<User> _userManager;

        public ChatController(GeminiService geminiService, MessageRepository messageRepository, UserManager<User> userManager)
        {
            _geminiService = geminiService;
            _messageRepository = messageRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("chat")]
        public async Task<IActionResult> ChatWithGemini([FromBody] ChatRequestDto requestDto)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Content))
                return BadRequest(new { message = "Message content is required." });

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized(new { message = "User not authenticated. Please log in." });
                }

                var userMessage = await _messageRepository.AddMessageAsync(new Message
                {
                    UserId = user.Id,
                    Content = requestDto.Content,
                    CreatedAt = DateTime.UtcNow
                });

                var messages = await _messageRepository.GetAllMessagesAsync();
                string aiResponse = await _geminiService.GetGeminiResponse(messages);

                var aiMessage = await _messageRepository.AddMessageAsync(new Message
                {
                    Content = aiResponse,
                    CreatedAt = DateTime.UtcNow
                });

                return Ok(new { userMessage, aiMessage });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await _messageRepository.GetAllMessagesAsync();
            return Ok(messages);
        }

        [HttpPut("messages/{messageId}")]
        public async Task<IActionResult> UpdateMessage(int messageId, [FromBody] UpdateMessageDto updateDto)
        {
            var updatedMessage = await _messageRepository.UpdateMessageAsync(messageId, updateDto.Content);
            if (updatedMessage == null) return NotFound(new { message = "Message not found" });
            return Ok(updatedMessage);
        }

        [HttpDelete("messages/{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var result = await _messageRepository.DeleteMessageAsync(messageId);
            if (!result) return NotFound(new { message = "Message not found" });
            return Ok(new { message = "Message deleted successfully" });
        }
    }
}
