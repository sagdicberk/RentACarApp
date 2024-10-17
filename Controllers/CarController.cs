using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RentaCarApp.Data.Abstracts;
using RentaCarApp.Models;


namespace RentaCarApp.Controllers
{
    [Route("Car")]
    public class CarController : Controller
    {

        private readonly CarRepository _repository;

        public CarController(CarRepository repository)
        {
            _repository = repository;
        }

        /*
            User kullanıcısı için hazırlanmış araç listesi bulunduran bir sayfadır.
            searchTerm'e göre model ve marka filtreleme özelliği vardır.
            brand'a göre de filtreleme yapılabilir.
        */

        [HttpGet("Index")]
        public async Task<IActionResult> Index(string searchTerm, string brand)
        {
            var Cars = await _repository.ActiveCars(searchTerm).ToListAsync();

            if (!string.IsNullOrEmpty(brand))
            {
                Cars = await _repository.GetCarsByBrand(brand).ToListAsync();
            }

            return View(Cars);
        }

        /*
            Admin kullanıcısı için hazırlanmış araç listesidir.
            searchTerm'e göre model ve marka filtreleme özelliği vardır.
        */

        [HttpGet("List")]
        public async Task<IActionResult> List(string searchTerm)
        {
            var Cars = await _repository.cars(searchTerm).ToListAsync();
            return View(Cars);
        }

        /*
            User kullanıcısı için hazırlanıştır.
            Araçları Idsine göre Detail sayfasını getirir.
            Araç Id'si eşleşmeli ve Aktif olmalı
        */
        [HttpGet("Detail/{CarId}")]
        public async Task<IActionResult> Detail(int CarId)
        {
            var Car = await _repository.GetCarDetailsById(CarId);
            if (Car == null)
            {
                return NotFound();
            }
            return View(Car);
        }

        // Create işlemi

        /*
            Admin kullanıcısı için yapılmıştır.
            List Actionu üzerinden Ulaşılabilir.
            Create işlemi için gerekli form sayfasını getirir.

        */

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        /*
            Admin kullanıcısı iiçin yapılmıştır.
            Create işlemini gerçekleştiren yer burasıdır.
            Bir Car modeli ve dosya alır.
            Gerekli kontrolleri yaptıktan sonra modeli Database'e kaydeder. 
        */

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car NewCar, IFormFile imageFile)
        {
            // Resim dosyasını kontrol et
            if (imageFile == null)
            {
                ModelState.AddModelError("", "Lütfen bir resim seçin");
                return View(NewCar);
            }

            // Model geçerliliğini kontrol et
            if (ModelState.IsValid)
            {
                try
                {
                    // Yeni aracı oluştur
                    await _repository.CreateCar(NewCar, imageFile);
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    // Hata durumunda hata mesajını ModelState'e ekle
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Hatalı model durumunda veya hata fırlatıldığında, aynı sayfayı döndür
            return View(NewCar);
        }

        // Update 
        /*
            Admin kullanıcısı için hazırlanmıştır.
            Gerekli modeli Id'sine göre getirir
            ve update formuna yerleştirir.
        */
        [HttpGet("Update/{CarId}")]
        public async Task<IActionResult> Update(int CarId)
        {
            var car = await _repository.GetCarDetailsById(CarId);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }


        /*
            Admin kullanıcısı için hazırlanmıştır.
            Form içerinden CarId değerini alır
            Car Modeli ve image file dğerlerinin 
            validation işlemlerini yaparak verilen
            CarId değerindeki modeli günceller
        */
        [HttpPost("Update/{CarId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int CarId, [FromForm] Car car, IFormFile imageFile)
        {

            // Model geçerliliğini kontrol et
            if (ModelState.IsValid)
            {
                try
                {
                    // Güncelleme işlemini gerçekleştir
                    await _repository.Update(CarId, car, imageFile);
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    // Hata durumunda hata mesajını ModelState'e ekle
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Hatalı model durumunda, aynı sayfayı döndür
            return View(car);
        }

        // delete 
        /*
            Admin kullanıcısı için hazırlanmıştır.
            Delete işlemi için bir onay sayfasına gider.
            ve onay sayfasında silinecek modeli gösterir.
        */
        [HttpGet]
        public async Task<IActionResult> Delete(int? CarId)
        {
            if (CarId == null)
            {
                return NotFound();
            }

            var car = await _repository.GetCarDetailsById(CarId);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        /*
            Admin kullanıcısı için hazırlanmıştır.
            seçilen modeli CarId'ye göre silme işlemi gerçekleştirir.
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int CarId)
        {
            await _repository.Delete(CarId);
            return RedirectToAction("List");
        }




    }
}