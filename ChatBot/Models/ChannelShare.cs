using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class ChannelShare
    {
        [Key]
        public int ShareId { get; set; }
        public int ChannelId { get; set; }
        public int UserId { get; set; }
        public string Permission { get; set; } 
        public DateTime SharedAt { get; set; }
        public Channel Channel { get; set; }
        public User User { get; set; }
    }
}
