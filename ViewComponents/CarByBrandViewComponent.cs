using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentaCarApp.Data.Abstracts;


namespace RentACarApp.ViewComponents
{
    public class CarByBrandViewComponent : ViewComponent
    {
        private readonly CarRepository _repository;

        public CarByBrandViewComponent(CarRepository repository){
            _repository = repository;
        }




        public async Task<IViewComponentResult> InvokeAsync(int BrandId)
        {
            var Cars = await _repository.GetCarsByBrand(BrandId).Take(3).ToListAsync(); 
            return View(Cars); 
        }
    }
}