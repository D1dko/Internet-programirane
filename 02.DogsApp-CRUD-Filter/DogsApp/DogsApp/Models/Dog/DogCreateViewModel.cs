using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DogsApp.Models.Dog
{
    public class DogCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Range(0, 30, ErrorMessage = "Age must be between 0 and 30")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Breed")]
        public string Breed { get; set; } = null!;

        [Display(Name = "Dog Picture")]
        public string? Picture { get; set; }
    }
}
