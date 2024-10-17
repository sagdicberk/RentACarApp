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

        [HttpGet("List")]
        public async Task<IActionResult> List(string searchTerm)
        {
            var Cars = await _repository.cars(searchTerm).ToListAsync();
            return View(Cars);
        }


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

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car NewCar, IFormFile imageFile)
        {
            if (imageFile == null)
            {
                ModelState.AddModelError("", "Lütfen bir resim seçin");
                return View(NewCar);
            }


            if (ModelState.IsValid)
            {
                await _repository.CreateCar(NewCar, imageFile);
                return RedirectToAction("List");
            }
            return View(NewCar);
        }

        // Update 
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

        [HttpPost("Update/{CarId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int CarId, [FromForm] Car car, IFormFile imageFile)
        {

            if (ModelState.IsValid)
            {
                await _repository.Update(CarId, car, imageFile);
                return RedirectToAction("List");
            }

            return View(car);
        }

        // delete 

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int CarId)
        {
            await _repository.Delete(CarId);
            return RedirectToAction("List");
        }




    }
}