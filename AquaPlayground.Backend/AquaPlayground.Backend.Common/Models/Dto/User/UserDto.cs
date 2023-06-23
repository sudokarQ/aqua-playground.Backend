namespace AquaPlayground.Backend.Common.Models.Dto.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserDto : UserLoginDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; } = new List<string>() {"User"};    
    }
}
