using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentACarApp.Models;

namespace RentACarApp.Data.Abstracts
{
    public interface BrandRepository
    {
        Task<IEnumerable<Brand>> GetAllBrands(); // Tüm markaları getir
        Task<Brand?> GetBrandByIdAsync(int? id); // ID'ye göre marka getir
        Task AddBrandAsync(Brand brand); // Yeni marka ekle
        Task UpdateBrandAsync(int Id, Brand brand); // Marka güncelle
        Task DeleteBrandAsync(int id);
    }
}