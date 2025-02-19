using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class Channel
    {
        [Key]
        public int ChannelId { get; set; }
        public string OwnerId { get; set; }
        public int ModelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public string CustomParameters { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User Owner { get; set; }
        public Model Model { get; set; }
    }
}
