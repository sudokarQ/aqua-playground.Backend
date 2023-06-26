namespace AquaPlayground.Backend.Common.Models.Dto.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserUpdateDto
    {
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
    }
}
