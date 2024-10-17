using RentaCarApp.Models;

namespace RentaCarApp.Data.Abstracts{

    public interface CarRepository{
        
        Task CreateCar(Car NewCar, IFormFile imageFile);

        IQueryable<Car> cars (string searchTerm);

        IQueryable<Car> ActiveCars (string searchTerm);

        Task<Car?> GetCarDetailsById(int? CarId);

        Task Update(int CarId, Car car, IFormFile imageFile);

        Task Delete(int? CarId);

        IQueryable<Car> GetCarsByBrand(string brand);
        IQueryable<string> GetAllBrands();
        
        
    }
}

