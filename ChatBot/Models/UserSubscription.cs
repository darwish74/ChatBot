using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class UserSubscription
    {
        [Key]
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } // active, expired, canceled
        public User User { get; set; }
        public SubscriptionPlan Plan { get; set; }
    }
}
