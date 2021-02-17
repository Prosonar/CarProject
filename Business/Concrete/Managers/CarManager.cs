using Business.Abstract.Services;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete.Managers
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            if(car.Name.Length<=1 || car.DailyPrice<=0)
            {
                return;
            }
            _carDal.Add(car);
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public Car GetCarById(int carId)
        {
            return _carDal.GetById(c => c.Id == carId);
        }

        public List<Car> GetCars(Expression<Func<Car, bool>> filter = null)
        {
            return _carDal.GetAll(filter);
        }

        public List<Car> GetCarsByBrand(int brandId)
        {
            return _carDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetCarsByColor(int colorId)
        {
            return _carDal.GetAll(c => c.ColorId == colorId);
        }

        public List<CarDetail> GetCarWithDetails()
        {
            var result = _carDal.GetCarsWithDetails();
            return result.ToList();
        }

        public void Update(Car car)
        {
            if (car.Name.Length <= 1 || car.DailyPrice <= 0)
            {
                return;
            }
            _carDal.Update(car);
        }
    }
}
