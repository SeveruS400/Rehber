using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record UserWithRolesDto
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "UserName is required.")]
        public String? UserName { get; init; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        public String? Email { get; init; }
        public HashSet<String> Roles { get; set; } = new HashSet<string>();
        public string AddedByUserName { get; set; }

    }
}
