using Microsoft.AspNetCore.Mvc;
using RentACarApp.Data.Abstracts;

namespace RentaCarApp.ViewComponents
{
    public class BrandListViewComponent : ViewComponent
    {
        private readonly BrandRepository _repository;

        public BrandListViewComponent(BrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _repository.GetAllBrands(); 
            return View(brands); 
        }
    }
}
