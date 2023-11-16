using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsApp.Infrastructure.Data.Domain
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;


        [Range(0, 30)]
        public int Age { get; set; }

        [Required]
        [MaxLength(50)]
        public string Breed { get; set; } = null!;

        public string? Picture { get; set; }
    }
}
