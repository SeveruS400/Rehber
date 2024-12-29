using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record ResetPasswordDto
    {
        public String? UserName { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Parola girilmesi zorunludur")]
        [MinLength(6, ErrorMessage = "Parola en az 6 haneli olmalıdır")]
        public String? Password { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Parola Doğrulama girilmesi zorunludur")]
        [Compare("Password", ErrorMessage = "Parolalar birbiri ile uyuşmamaktadır")]
        public String? ConfirmPassword { get; init; }
    }
}
