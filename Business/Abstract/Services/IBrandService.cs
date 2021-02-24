using Core.Utilities.Results.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IBrandService
    {
        IDataResult<List<Brand>> GetBrands(Expression<Func<Brand, bool>> filter = null);
        IDataResult<Brand> GetById(int brandId);
        IResult Add(Brand brand);
        IResult Delete(Brand brand);
        IResult Update(Brand brand);
    }
}
