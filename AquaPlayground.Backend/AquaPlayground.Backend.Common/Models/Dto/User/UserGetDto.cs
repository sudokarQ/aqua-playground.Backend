namespace AquaPlayground.Backend.Common.Models.Dto.User
{
    public class UserGetDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
