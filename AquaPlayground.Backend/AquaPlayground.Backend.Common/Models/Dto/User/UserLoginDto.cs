namespace AquaPlayground.Backend.Common.Models.Dto.User
{
    using System.ComponentModel.DataAnnotations;

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
