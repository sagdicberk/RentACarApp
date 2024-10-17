using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RentaCarApp.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Marka gereklidir.")]
        [StringLength(50, ErrorMessage = "Marka 50 karakterden uzun olmamalıdır.")]
        [Display(Name ="Marka Adı")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model gereklidir.")]
        [StringLength(50, ErrorMessage = "Model 50 karakterden uzun olmamalıdır.")]
        [Display(Name ="Model Adı")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Günlük fiyat gereklidir.")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        [Display(Name ="Günlük Fiyatı")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Yıl gereklidir.")]
        [Range(1990, int.MaxValue, ErrorMessage = "Yıl 1990'dan önce olamaz.")]
        [Display(Name ="Üretim Yılı")]
        public int Year { get; set; }

        [Display(Name ="Görsel")]
        public string? Image { get; set; } = string.Empty;

        [Display(Name ="Aktif mi")]
        public bool IsActive { get; set; }


    }
}