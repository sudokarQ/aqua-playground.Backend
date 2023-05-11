using System.ComponentModel.DataAnnotations;

namespace AquaPlayground.Backend.Common.Models.Dto
{
    public class IdDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
