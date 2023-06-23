namespace AquaPlayground.Backend.Common.Models.Dto.Service
{
    using System.ComponentModel.DataAnnotations;

    public class ServicePutDto
    {
        [Required]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Incorrect price")]
        public decimal? Price { get; set; }
    }
}
