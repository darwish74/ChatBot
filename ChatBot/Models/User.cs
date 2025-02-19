using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace ChatBot.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<UserSocialMedia> SocialMediaLinks { get; set; } = new List<UserSocialMedia>();
        public ICollection<UserSubscription> Subscriptions { get; set; }
        public ICollection<Channel> Channels { get; set; }
    }
}
