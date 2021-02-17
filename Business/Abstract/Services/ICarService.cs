using Entity.Concrete;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface ICarService
    {
        List<Car> GetCars(Expression<Func<Car, bool>> filter = null);
        List<Car> GetCarsByBrand(int brandId);
        List<Car> GetCarsByColor(int colorId);
        List<CarDetail> GetCarWithDetails();
        Car GetCarById(int carId);
        void Add(Car car);
        void Delete(Car car);
        void Update(Car car);
    }
}
