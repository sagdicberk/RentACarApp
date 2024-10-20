using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set;}

        [Required(ErrorMessage = "Marka adı gereklidir.")]
        [StringLength(50, ErrorMessage = "Marka adı 50 karakterden uzun olmamalıdır.")]
        [Display(Name = "Marka Adı")]
        public string Name { get; set; } = string.Empty;
    }
}