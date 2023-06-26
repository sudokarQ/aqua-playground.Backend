namespace AquaPlayground.Backend.Common.Models.Dto.Service
{
    using System.ComponentModel.DataAnnotations;

    public class ServicePostDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string TypeService { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Некорректная стоимость")]
        public decimal Price { get; set; }
    }
}
