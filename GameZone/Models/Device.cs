using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Device
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(250)]
        public string? Icon { get; set; }


        public ICollection<GameDevice> GameDevices { get; set; } = default!;

    }
}
