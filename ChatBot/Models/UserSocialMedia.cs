using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class UserSocialMedia
    {
        [Key]
        public int SocialId { get; set; }
        public string UserId { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public User User { get; set; }
    }
}
