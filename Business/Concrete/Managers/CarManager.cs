using Business.Abstract.Services;
using Core.Utilities.ExceptionHandle;
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
            if (car.Name.Length<=1 || car.DailyPrice <= 0)
            {
                return new ErrorResult("Validasyon hatası!");
            }
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _carDal.Add(car);
            });
            if(!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("Araba eklendi.");
        }

        public IResult Delete(Car car)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _carDal.Delete(car);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("Araba silindi.");
        }

        public IDataResult<Car> GetCarById(int carId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, Car>((x) =>
            {
                return _carDal.GetById(c => c.Id == x);
            },carId);
            if(!result.Success)
            {
                return new ErrorDataResult<Car>("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<Car>(result.Data,"Araba bulundu!");
        }

        public IDataResult<List<Car>> GetCars(Expression<Func<Car, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<Car, bool>>, List<Car>>((x) =>
            {
                return _carDal.GetAll(x);
            },filter);
            if(!result.Success)
            {
                return new ErrorDataResult<List<Car>>("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<Car>>(result.Data,"Filtrelemeye uygun arabalar listelendi.");
        }

        public IDataResult<List<Car>> GetCarsByBrand(int brandId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, List<Car>>((x) =>
            {
                return _carDal.GetAll(c => c.BrandId == x);
            }, brandId);
            if (!result.Success)
            {
                return new ErrorDataResult<List<Car>>("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<Car>>(result.Data, "Uygun arabalar listelendi!");
        }

        public IDataResult<List<Car>> GetCarsByColor(int colorId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, List<Car>>((x) =>
            {
                return _carDal.GetAll(c => c.ColorId == x);
            }, colorId);
            if (!result.Success)
            {
                return new ErrorDataResult<List<Car>>("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<Car>>(result.Data, "Uygun arabalar listelendi!");
        }

        public IDataResult<List<CarDetail>> GetCarWithDetails()
        {
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<CarDetail>>(() =>
            {
                return _carDal.GetCarsWithDetails();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<CarDetail>>("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<CarDetail>>(result.Data, "Uygun arabalar listelendi!");
        }

        public IResult Update(Car car)
        {
            if (car.Name.Length <= 1 || car.DailyPrice <= 0)
            {
                return new ErrorResult("Validasyon hatası.");
            }
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _carDal.Update(car);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("Araba güncellendi.");
        }
    }
}
