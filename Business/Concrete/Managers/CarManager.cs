using Business.Abstract.Services;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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

        public IResult Add(Car car)
        {
            if(car.Name.Length<=1 || car.DailyPrice<=0)
            {
                return new ErrorResult("Validasyon hatası!");
            }
            _carDal.Add(car);
            return new SuccessResult("Araba eklendi.");
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult("Araba silindi.");
        }

        public IDataResult<Car> GetCarById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.GetById(c => c.Id == carId),"Araba bulundu!");
        }

        public IDataResult<List<Car>> GetCars(Expression<Func<Car, bool>> filter = null)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(filter),"Filtrelemeye uygun arabalar listelendi.");
        }

        public IDataResult<List<Car>> GetCarsByBrand(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId),"Markaya göre arabalar listelendi.");
        }

        public IDataResult<List<Car>> GetCarsByColor(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId),"Renge göre arabalar listelendi.");
        }

        public IDataResult<List<CarDetail>> GetCarWithDetails()
        {
            var result = _carDal.GetCarsWithDetails();
            return new SuccessDataResult<List<CarDetail>>(result.ToList(),"Arabalar detaylarıyla beraber listelendi.");
        }

        public IResult Update(Car car)
        {
            if (car.Name.Length <= 1 || car.DailyPrice <= 0)
            {
                return new ErrorResult("Validasyon hatası.");
            }
            _carDal.Update(car);
            return new SuccessResult("Araba güncellendi.");
        }
    }
}
