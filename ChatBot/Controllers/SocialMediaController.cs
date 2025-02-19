using ChatBot.Data;
using ChatBot.DTOS;
using ChatBot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ChatBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext dbContext;

        public SocialMediaController(UserManager<User> userManager,ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpPost("add-social-link")]
        public async Task<IActionResult> AddSocialLink([FromBody] SocialMediaLinkDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return NotFound(new { message = "User not found" });
            var socialLink = new UserSocialMedia
            {
                UserId = user.Id,
                Platform = model.Platform,
                Url = model.Url
            };
            user.SocialMediaLinks.Add(socialLink);
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(new { message = "Social link added successfully!" });

            return BadRequest(result.Errors);
        }
        [HttpDelete("remove-social-link")]
        public async Task<IActionResult> RemoveSocialLink([FromBody] SocialMediaLinkDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return NotFound(new { message = "User not found" });
            var linkToRemove = dbContext.UserSocialMedias.FirstOrDefault(m => m.User.UserName == model.UserName && m.Platform == model.Platform);
            if (linkToRemove == null)
                return NotFound(new { message = "Social link not found" });
            user.SocialMediaLinks.Remove(linkToRemove);
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(new { message = "Social link removed successfully!" });
            return BadRequest(result.Errors);
        }
        [HttpGet("get-social-links/{username}")]
        public async Task<IActionResult> GetSocialLinks(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound(new { message = "User not found" });
            var socialLinks = dbContext.UserSocialMedias.Where(u => u.User.UserName == user.UserName).Select(e => new
            {
               e.User.UserName,
               e.Url,
               e.Platform
            });
            return Ok(socialLinks);
        }
    }
}
