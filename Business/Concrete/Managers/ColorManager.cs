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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete.Managers
{
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;
        private ICarService _carService;

        public ColorManager(IColorDal colorDal,ICarService carService)
        {
            _colorDal = colorDal;
            _carService = carService;
        }

        [ValidationAspect(typeof(ColorValidator),Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IColorService.Get", Priority = 3)]
        public IResult Add(Color color)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _colorDal.Add(color);
            });
            if(!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IColorService.Get", Priority = 3)]
        public IResult Delete(Color color)
        {
            
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                var listOfCar = _carService.GetCars(c => c.ColorId == color.Id).Data.ToList();
                if (listOfCar.Count > 0)
                {
                    throw new Exception(Messages.ForeignKeyMessage);
                }
                _colorDal.Delete(color);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<Color> GetById(int colorId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, Color>((x) =>
            {
                 return _colorDal.GetById(c => c.Id == x);
            },colorId);
            if(!result.Success)
            {
                return new ErrorDataResult<Color>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<Color>(_colorDal.GetById(c => c.Id == colorId), Messages.SuccessMessage);
        }

        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<Color>> GetColors(Expression<Func<Color, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<Color, bool>>, List<Color>>((f) =>
            {
                return _colorDal.GetAll(f);
            },filter);
            if(!result.Success)
            {
                return new ErrorDataResult<List<Color>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(filter), Messages.SuccessMessage);
        }

        [ValidationAspect(typeof(ColorValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IColorService.Get", Priority = 3)]
        public IResult Update(Color color)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _colorDal.Update(color);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
    }
}
