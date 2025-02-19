using ChatBot.Models;
using ChatBot.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly UserSubscriptionRepository _userSubscriptionRepository;

        public SubscriptionController(SubscriptionPlanRepository subscriptionPlanRepository, UserSubscriptionRepository userSubscriptionRepository)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _userSubscriptionRepository = userSubscriptionRepository;
        }

        [HttpGet("plans")]
        public async Task<IActionResult> GetSubscriptionPlans()
        {
            var plans = await _subscriptionPlanRepository.GetAllPlansAsync();
            return Ok(plans);
        }

        [HttpPost("plans")]
        public async Task<IActionResult> CreateSubscriptionPlan([FromBody] SubscriptionPlan plan)
        {
            var newPlan = await _subscriptionPlanRepository.AddPlanAsync(plan);
            return CreatedAtAction(nameof(GetSubscriptionPlans), new { id = newPlan.PlanId }, newPlan);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeUser([FromBody] UserSubscription subscription)
        {
            subscription.StartDate = DateTime.UtcNow;
            subscription.EndDate = DateTime.UtcNow.AddMonths(1);
            subscription.Status = "active";
            var newSubscription = await _userSubscriptionRepository.SubscribeUserAsync(subscription);
            return Ok(newSubscription);
        }

        [HttpGet("subscriptions/{userId}")]
        public async Task<IActionResult> GetUserSubscription(int userId)
        {
            var subscription = await _userSubscriptionRepository.GetUserSubscriptionsAsync(userId);
            if (subscription == null) return NotFound(new { message = "No active subscription found." });
            return Ok(subscription);
        }

        [HttpPut("subscriptions/cancel/{userId}")]
        public async Task<IActionResult> CancelSubscription(int userId)
        {
            var result = await _userSubscriptionRepository.CancelSubscriptionAsync(userId);
            if (!result) return NotFound(new { message = "No active subscription found to cancel." });
            return Ok(new { message = "Subscription canceled successfully." });
        }
    }
}
