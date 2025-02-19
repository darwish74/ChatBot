using System.ComponentModel.DataAnnotations;

namespace ChatBot.DTOS
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
