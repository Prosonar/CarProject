using Business.Abstract.Services;
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
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;
        private ICarService _carService;

        public BrandManager(IBrandDal brandDal,ICarService carService)
        {
            _brandDal = brandDal;
            _carService = carService;
        }

        public IResult Add(Brand brand)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _brandDal.Add(brand);
            });
            if(!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("Marka eklendi!");
        }

        public IResult Delete(Brand brand)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                var listOfCars = _carService.GetCars(c => c.BrandId == brand.Id).Data.Select(c => c.BrandId).ToList();
                if(listOfCars.Count>0)
                {
                    throw new Exception("Bu markada araba mevcut silme işlemi başarısız!");
                }
                _brandDal.Delete(brand);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("Marka silindi!");
        }

        public IDataResult<List<Brand>> GetBrands(Expression<Func<Brand, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<Brand, bool>>, List<Brand>>((f) =>
            {
                return _brandDal.GetAll(f);
            }, filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<Brand>>("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(filter), "Markalar listelendi.");
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, Brand>((x) =>
            {
                return _brandDal.GetById(b => b.Id == x);
            },brandId);
            if (!result.Success)
            {
                return new ErrorDataResult<Brand>("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<Brand>(_brandDal.GetById(b => b.Id == brandId), "Marka bulundu!");
        }

        public IResult Update(Brand brand)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _brandDal.Update(brand);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("Marka güncellendi!");
        }
    }
}
