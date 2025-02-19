using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class SubscriptionPlan
    {
        [Key]
        public int PlanId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string Features { get; set; }
    }
}
