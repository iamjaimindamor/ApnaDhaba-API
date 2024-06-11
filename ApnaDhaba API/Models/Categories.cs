using System.ComponentModel.DataAnnotations;

namespace ApnaDhaba_API.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}