using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class Conversation
    {
        [Key]
        public int ConversationId { get; set; }
        public int ChannelId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActivity { get; set; }
        public Channel Channel { get; set; }
    }
}
