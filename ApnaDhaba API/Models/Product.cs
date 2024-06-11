using System.ComponentModel.DataAnnotations;

namespace ApnaDhaba_API.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string? ImageURL { get; set; }
        public Categories? CategoryId { get; set; }
    }
}