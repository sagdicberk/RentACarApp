using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using RentaCarApp.Models;
using RentACarApp.Data.Abstracts;
using RentACarApp.Models;

namespace RentACarApp.Data.Concrete
{
    public class BrandRepositoryImp : BrandRepository
    {
        private readonly CarDbContext _context;

        public BrandRepositoryImp(CarDbContext context)
        {
            _context = context;
        }

        public async Task AddBrandAsync(Brand brand)
        {
            try
            {
                // Markanın zaten var olup olmadığını kontrol et
                var existBrand = await _context.Brands
                    .FirstOrDefaultAsync(b => b.Name == brand.Name);

                if (existBrand == null)
                {
                    await _context.Brands.AddAsync(brand);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Brand already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteBrandAsync(int id)
        {
            try
            {
                var brand = await GetBrandByIdAsync(id);
                if (brand != null)
                {
                    _context.Brands.Remove(brand);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Brand not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            try
            {
                return await _context.Brands.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Brand?> GetBrandByIdAsync(int? id)
        {   
            try
            {
                return await _context.Brands.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateBrandAsync(int id, Brand brand)
        {
            try
            {
                var existingBrand = await _context.Brands.FindAsync(id);
                if (existingBrand != null)
                {
                    existingBrand.Name = brand.Name; // Diğer alanları da güncelle
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Brand not found or ID mismatch.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}