using System.ComponentModel.DataAnnotations;

namespace ApnaDhaba_API.Models.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string? Username { get; set; }

        public string? Password { get; set; }
    }
}