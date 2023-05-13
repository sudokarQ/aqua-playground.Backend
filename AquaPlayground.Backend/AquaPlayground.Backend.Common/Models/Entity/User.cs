using Microsoft.AspNetCore.Identity;

namespace AquaPlayground.Backend.Common.Models.Entity
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }

        
        public List<Order> Orders { get; set; }
    }
}
