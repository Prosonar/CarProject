using Business.Abstract.Services;
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
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult("Marka eklendi!");
        }

        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult("Marka silindi!");
        }

        public IDataResult<List<Brand>> GetBrands(Expression<Func<Brand, bool>> filter = null)
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(filter), "Markalar listelendi.");
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.GetById(b => b.Id == brandId), "Marka bulundu!");
        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult("Marka güncellendi!");
        }
    }
}
