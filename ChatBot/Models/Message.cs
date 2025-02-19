using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string? UserId { get; set; }
        public int? ModelId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
    }
}
