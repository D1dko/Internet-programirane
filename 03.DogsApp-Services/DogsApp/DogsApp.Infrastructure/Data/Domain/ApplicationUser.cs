using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace DogsApp.Infrastructure.Data.Domain
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; } = null!;
    }
}
