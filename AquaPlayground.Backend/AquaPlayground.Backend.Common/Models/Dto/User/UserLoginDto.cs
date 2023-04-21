using System.ComponentModel.DataAnnotations;

namespace AquaPlayground.Backend.Common.Models.Dto.User
{
    public class UserLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
