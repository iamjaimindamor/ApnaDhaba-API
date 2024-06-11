using System.ComponentModel.DataAnnotations;

namespace ApnaDhaba_API.Models
{
    public class roleModel
    {
        [Key]
        public int roleId { get; set; }

        public string? roleName { get; set; }
    }
}