using System.ComponentModel.DataAnnotations;

namespace AquaPlayground.Backend.Common.Models.Dto.Service
{
    public class ServicePutDto : IdDto
    {
        [Required]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Incorrect price")]
        public decimal? Price { get; set; }
    }
}
