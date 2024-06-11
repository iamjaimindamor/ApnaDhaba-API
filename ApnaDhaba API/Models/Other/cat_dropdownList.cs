using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ApnaDhaba_API.Models.Other
{
    public class cat_dropdownList
    {
        [Key]
        public int Id { get; set; }

        public List<SelectListItem>? categoryDropDown { get; set; }
    }
}