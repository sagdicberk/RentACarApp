using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentACarApp.Data.Abstracts;
using RentACarApp.Models;

namespace RentACarApp.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandRepository _brandRepository;

        public BrandController(BrandRepository repository)
        {
            _brandRepository = repository;
        }

        /*
            Marka oluşturmak için gerekli Controller yapısı
        */

        // Get Methodu ile forma gidiyoruz
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _brandRepository.AddBrandAsync(brand);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Eğer model geçerli değilse, hataları göster
            return View(brand);
        }


        /*
            Delete işlemi için gerekli get ve post işlemi
        */
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm] int id)
        {
            await _brandRepository.DeleteBrandAsync(id);
            return RedirectToAction("Index");
        }

        // index sayfasını çağırmak için
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var brands = await _brandRepository.GetAllBrands();
            return View(brands);
        }

        /*
            Update işlemi için gerekli get ve post işlemi
        */
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var brand = await _brandRepository.GetBrandByIdAsync(id);
                return View(brand);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] int id, Brand brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingBrand = await _brandRepository.GetBrandByIdAsync(id);
                    if (existingBrand == null)
                    {
                        return NotFound();
                    }

                    await _brandRepository.UpdateBrandAsync(id, brand);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "Güncellenirken bir hata oluştu: " + ex.Message);

                }

            }
            return View(brand);
        }


    }
}