using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentaCarApp.Data.Abstracts;

namespace RentaCarApp.ViewComponents
{
    public class BrandListViewComponent : ViewComponent
    {
        private readonly CarRepository _repository;

        public BrandListViewComponent(CarRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _repository.GetAllBrands().ToListAsync(); 
            return View(brands); 
        }
    }
}
