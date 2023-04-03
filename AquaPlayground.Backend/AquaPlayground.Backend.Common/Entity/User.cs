namespace AquaPlayground.Backend.Common.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login {get; set;}
        public string Password { get; set;}
        public string Email { get; set;}
        public string PhoneNumber { get; set;}
        public string Name { get; set;}
        public string Surname { get; set;}  
        public DateOnly BirthDay { get; set;}

        public List<Order> Orders { get; set; }
    }
}
