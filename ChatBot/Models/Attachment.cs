using System.ComponentModel.DataAnnotations;

namespace ChatBot.Models
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }
        public int MessageId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public DateTime UploadedAt { get; set; }
        public Message Message { get; set; }
    }
}
