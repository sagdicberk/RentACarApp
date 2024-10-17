using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using RentaCarApp.Data.Abstracts;
using RentaCarApp.Models;
using SQLitePCL;

namespace RentaCarApp.Data.concreate
{

    public class CarRepositoryImp : CarRepository
    {

        private readonly CarDbContext _context;

        public CarRepositoryImp(CarDbContext context)
        {
            _context = context;
        }

        /*
            Admin CarList için hazırlanmış
            Arama işlemi yapabileceğimiz bir 
            fonksiyondur. LİNQ sorgusu ile 
            Model ve Marka için sorgu yapabiliriz.
        */
        public IQueryable<Car> cars(string searchTerm)
        {
            return _context.Cars
                .Where(c => string.IsNullOrEmpty(searchTerm)
                            || c.Brand.ToLower().Contains(searchTerm)
                            || c.Model.ToLower().Contains(searchTerm));
        }

        /*
            User CarList için arama yapmamızı sağlayan bir 
            LİNQ sorgu fonksiyonudur.
        */
        public IQueryable<Car> ActiveCars(string searchTerm)
        {
            return _context.Cars
                .Where(c => c.IsActive &&
                            (string.IsNullOrEmpty(searchTerm)
                            || c.Brand.ToLower().Contains(searchTerm)
                            || c.Model.ToLower().Contains(searchTerm)));
        }

        /*
            User CarList için hazırlanmıştır.
            Araçları markasına göre listeleyen 
            LİNQ sorgusuna sahip bir fonksiyondur.
        */
        public IQueryable<Car> GetCarsByBrand(string brand)
        {
            return _context.Cars.Where(c => c.Brand == brand && c.IsActive);
        }

        /*
            Kullanıcı arayüzünde göstermek
            için hazırlanmıştır. Araçların 
            markalarını getiren bir LINQ 
            sorgusuna sahip bir fonksiyondur.
        */
        public IQueryable<string> GetAllBrands()
        {
            return _context.Cars.Select(car => car.Brand).Distinct();
        }

        /*
            Admin kullanıcısı için hazırlanmıştır.
            Yeni bir Car modeli ve bir resim dosyası alır. 
            Resim dosyasını işler ve Car modelini veritabanına ekler.
        */
        public async Task CreateCar(Car NewCar, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                var imageName = await ProcessImageFile(imageFile);
                if (imageName != null)
                {
                    NewCar.Image = imageName;
                }
            }
            else
            {
                throw new Exception("Lütfen bir dosya giriniz.");
            }

            _context.Cars.Add(NewCar);
            _context.SaveChanges();


        }

        /*
            Admin kullanıcısı için hazırlanmıştır.
            Verilen CarId'ye sahip Car modelini 
            veritabanından siler.
        */
        public async Task Delete(int? CarId)
        {
            var car = await _context.Cars.FindAsync(CarId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }

        }

        /*
            Verilen CarId'ye göre aktif bir aracı 
            veritabanından getirir. 
            Eğer araç mevcutsa detaylarını döner, 
            aksi takdirde null döner.
        */
        public async Task<Car?> GetCarDetailsById(int? CarId)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == CarId && c.IsActive);
            if (car != null)
            {
                return car;
            }
            return null;
        }

        /*
            Admin kullanıcısı için hazırlanmıştır.
            Verilen CarId'ye sahip aracı günceller. 
            Ayrıca yeni bir resim dosyası yüklenebilir.
        */
        public async Task Update(int carId, Car car, IFormFile imageFile)
        {
            var existingCar = await _context.Cars.FindAsync(carId); // Asenkron olarak bul
            if (existingCar != null)
            {
                if (imageFile != null)
                {
                    var imageResult = await ProcessImageFile(imageFile);
                    if (imageResult == null)
                    {
                        // Hata durumu
                        throw new Exception("Resim yüklenirken bir hata oluştu.");
                    }
                    existingCar.Image = imageResult;
                }

                // Diğer car güncellemelerini yap
                existingCar.PricePerDay = car.PricePerDay;
                existingCar.Year = car.Year;
                existingCar.Brand = car.Brand;
                existingCar.Model = car.Model;
                existingCar.IsActive = car.IsActive;

                _context.Cars.Update(existingCar);
                await _context.SaveChangesAsync(); // Asenkron olarak kaydet
            }
        }




        /*
            Resimlerin işlenmesi için gerekli yardımcı fonksiyondur. 
            Geçerli dosya uzantılarını kontrol eder ve dosyayı 
            belirtilen dizine kaydeder.
        */
        private async Task<string?> ProcessImageFile(IFormFile imageFile)
        {
            // Geçerli dosya uzantıları
            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            const long maxFileSize = 2 * 1024 * 1024; // 2 MB

            // Dosya boyutunu kontrol et
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new Exception("Dosya bulunamadı"); // Dosya yoksa null döner

            }

            if (imageFile.Length > maxFileSize)
            {
                // Hata mesajı döndür
                throw new Exception("2B'tan küçük bir dosya giriniz");
            }

            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            // Dosya uzantısını kontrol et
            if (!allowedExtensions.Contains(extension))
            {
                // Hata mesajı döndür
                throw new Exception("Geçerli bir dosya tipi seçiniz.");
            }

            // Rastgele dosya adı oluştur
            var randomFileName = $"{Guid.NewGuid()}{extension}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

            // Dosyayı kaydet
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                // Hata mesajı döndür
                throw new Exception("Dosya yüklenirken bir hata oluştu " + ex.Message);

            }

            return randomFileName; // Dosya başarıyla yüklendiyse dosya adını döndür
        }

    }
}