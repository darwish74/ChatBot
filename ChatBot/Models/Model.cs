using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string BaseConfiguration { get; set; } // JSON formatted
        public string Version { get; set; }
        public string Provider { get; set; }
    }
}
