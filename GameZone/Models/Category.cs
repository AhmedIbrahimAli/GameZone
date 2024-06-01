using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string? Name { get; set; }


        public ICollection<Game>? Games { get; set; }
    }
}
