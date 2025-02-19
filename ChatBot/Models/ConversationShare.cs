using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class ConversationShare
    {
        [Key]
        public int ShareId { get; set; }
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public string Permission { get; set; }
        public DateTime SharedAt { get; set; }
        public Conversation Conversation { get; set; }
        public User User { get; set; }
    }
}
