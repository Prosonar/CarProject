using Business.Abstract.Services;
using Core.Utilities.BusinessRuleHandle;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete.Managers
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        private ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal,ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }

        public IResult Add(CarImage carImage)
        {
            var ruleResult = BusinessRuleHandler.CheckTheRules(CheckImageCount(carImage.CarId),CheckTheCarExist(carImage.CarId));
            if(ruleResult.Count>0)
            {
                return new ErrorResult("İş kurallarına takıldı.");
            }
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _carImageDal.Add(carImage);
            });
            if(!result)
            {
                return new ErrorResult("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı.");
        }

        public IResult Delete(CarImage carImage)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _carImageDal.Delete(carImage);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı.");
        }

        public IDataResult<List<CarImage>> GetAll(Expression<Func<CarImage, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<CarImage, bool>>, List<CarImage>>((x) =>
            {
                return _carImageDal.GetAll(x);
            },filter);
            if(!result.Success)
            {
                return new ErrorDataResult<List<CarImage>>("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<CarImage>>(result.Data, "İşlem başarılı.");
        }

        public IResult Update(CarImage carImage)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _carImageDal.Update(carImage);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı.");
        }
        private IResult CheckImageCount(int carId)
        {
            var images = _carImageDal.GetAll(x => x.CarId == carId);
            if(images.Count >= 5)
            {
                return new ErrorResult("Arabanın en fazla 5 resmi olabilir.");
            }
            return new SuccessResult("Resim eklendi.");
        }
        private IResult CheckTheCarExist(int carId)
        {
            var car = _carService.GetCarById(carId);
            if(car == null)
            {
                return new ErrorResult("Araba bulunamadı");
            }
            return new SuccessResult("Resim eklendi.");
        }
    }
}
