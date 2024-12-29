using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record UserDtoForCreation : UserWithRolesDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Parola girilmesi zorunludur")]
        [MinLength(6, ErrorMessage = "Parola en az 6 haneli olmalıdır")]
        public String? Password { get; init; }
        public int SessionTimeoutInHours { get; set; } = 2;
    }
}
