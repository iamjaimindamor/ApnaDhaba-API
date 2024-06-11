using System.ComponentModel.DataAnnotations;

namespace ApnaDhaba_API.Models
{
    public class AssignedRole
    {
        [Key]
        public int serialID { get; set; }

        [Required]
        public int? roleId { get; set; }

        [Required]
        public int? userId { get; set; }
    }
}