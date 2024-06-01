using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Game
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(2500)]
        public string? Description { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Cover { get; set; }





        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<GameDevice> Devices { get; set; } = default!;

    }
}
