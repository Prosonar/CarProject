using Business.Abstract.Services;
using Business.BusinessAspects.Autofac;
using Business.Utilities.Messages.TurkishMessages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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

        [ValidationAspect(typeof(CarValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("ICarService.Get", Priority = 3)]
        public IResult Add(Car car)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _carDal.Add(car);
            });
            if(!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }

        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("ICarService.Get", Priority = 3)]
        public IResult Delete(Car car)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _carDal.Delete(car);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<Car> GetCarById(int carId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, Car>((x) =>
            {
                return _carDal.GetById(c => c.Id == x);
            },carId);
            if(!result.Success)
            {
                return new ErrorDataResult<Car>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<Car>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<Car>> GetCars(Expression<Func<Car, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<Car, bool>>, List<Car>>((x) =>
            {
                return _carDal.GetAll(x);
            },filter);
            if(!result.Success)
            {
                return new ErrorDataResult<List<Car>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<Car>>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrand(int brandId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, List<Car>>((x) =>
            {
                return _carDal.GetAll(c => c.BrandId == x);
            }, brandId);
            if (!result.Success)
            {
                return new ErrorDataResult<List<Car>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<Car>>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColor(int colorId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, List<Car>>((x) =>
            {
                return _carDal.GetAll(c => c.ColorId == x);
            }, colorId);
            if (!result.Success)
            {
                return new ErrorDataResult<List<Car>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<Car>>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<CarDetail>> GetCarWithDetails()
        {
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<CarDetail>>(() =>
            {
                return _carDal.GetCarsWithDetails();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<CarDetail>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<CarDetail>>(result.Data, Messages.SuccessMessage);
        }

        [ValidationAspect(typeof(CarValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("ICarService.Get", Priority = 3)]
        public IResult Update(Car car)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _carDal.Update(car);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
    }
}
