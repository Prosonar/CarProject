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
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete.Managers
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICarService _carService;

        public RentalManager(IRentalDal rentalDal,ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }

        [ValidationAspect(typeof(OperationClaimValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IRentalService.Get", Priority = 3)]
        public IResult Add(Rental rental)
        {
            
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                Car car = _carService.GetCarById(rental.CarId).Data;
                if(!car.IsAvailable)
                {
                    throw new Exception(Messages.CarNotAvaiblable);
                }
                _rentalDal.Add(rental);
                _carService.Update(new Car
                {
                    Id = car.Id,
                    BrandId = car.BrandId,
                    ColorId = car.ColorId,
                    Name = car.Name,
                    Description = car.Description,
                    //ModelYear = car.ModelYear,
                    DailyPrice = car.DailyPrice,
                    IsAvailable = false
                });
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IRentalService.Get", Priority = 3)]
        public IResult Delete(Rental rental)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _rentalDal.Delete(rental);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<Rental>> GetAll(Expression<Func<Rental, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<Rental, bool>>, List<Rental>>((f) =>
            {
                return _rentalDal.GetAll(f);
            }, filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<Rental>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<Rental>>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<RentalDetail>> GetAllWithDetails(Expression<Func<Rental, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<Rental, bool>>, List<RentalDetail>>((f) =>
            {
                return _rentalDal.GetRentalWithDetail(f);
            }, filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<RentalDetail>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<RentalDetail>>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<Rental> GetById(int rentalId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, Rental>((x) =>
            {
                return _rentalDal.GetById(u => u.Id == rentalId);
            }, rentalId);
            if (!result.Success)
            {
                return new ErrorDataResult<Rental>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<Rental>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        public IResult ReturnCar(Rental rental)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                Car car = _carService.GetCarById(rental.CarId).Data;
                _rentalDal.Update(new Rental 
                { 
                    Id = rental.Id, 
                    ReturnDate=DateTime.Now,
                    RentDate=rental.RentDate,
                    CarId=rental.CarId,
                    CustomerId=rental.CustomerId
                });
                _carService.Update(new Car
                {
                    Id = car.Id,
                    BrandId = car.BrandId,
                    ColorId = car.ColorId,
                    Name = car.Name,
                    Description = car.Description,
                    //ModelYear = car.ModelYear,
                    DailyPrice = car.DailyPrice,
                    IsAvailable = true
                });
            });
            if(!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }

        [ValidationAspect(typeof(OperationClaimValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IRentalService.Get", Priority = 3)]
        public IResult Update(Rental rental)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _rentalDal.Update(rental);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
    }
}
